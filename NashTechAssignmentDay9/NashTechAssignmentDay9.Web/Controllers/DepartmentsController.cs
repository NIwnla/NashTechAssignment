using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Models;

namespace NashTechAssignmentDay9.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var departments = _departmentService.GetAll();
        if (departments.Count() <= 0 || departments == null)
        {
            return NotFound();
        }
        return Ok(departments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentDto departmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _departmentService.CreateAsync(departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while creating department.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Department {departmentDto.Name} created.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid? id, [FromBody] DepartmentDto departmentDto)
    {
        if (id == null)
        {
            ModelState.AddModelError("", "Require Id");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _departmentService.UpdateAsync(id.Value, departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while updating department.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Department updated.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid? id)
    {
        if (!ModelState.IsValid || id == null)
        {
            return BadRequest(ModelState);
        }
        if (!await _departmentService.DeleteAsync(id.Value))
        {
            ModelState.AddModelError("", "Something went wrong while deleting department.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Department deleted.");
        }
    }

}
