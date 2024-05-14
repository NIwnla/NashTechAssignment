using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectEmployeesController : ControllerBase
{
    private readonly IProjectEmployeeService _service;
    public ProjectEmployeesController(IProjectEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var projectEmployees = _service.GetAll();
        if (projectEmployees == null || projectEmployees.Count() <= 0)
        {
            return NotFound();
        }
        return Ok(projectEmployees);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectEmployee projectEmployee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _service.CreateAsync(projectEmployee))
        {
            ModelState.AddModelError("", "Something went wrong while creating projectEmployee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"ProjectEmployee created.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProjectEmployee projectEmployee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _service.UpdateAsync(projectEmployee))
        {
            ModelState.AddModelError("", "Something went wrong while updating projectEmployee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"ProjectEmployee updated.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid? projectId, Guid? employeeId)
    {
        if (projectId == null || employeeId == null)
        {
            ModelState.AddModelError("", "Require Ids");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _service.DeleteAsync(projectId.Value, employeeId.Value))
        {
            ModelState.AddModelError("", "Something went wrong while deleting projectEmployee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"ProjectEmployee deleted.");
        }
    }
}
