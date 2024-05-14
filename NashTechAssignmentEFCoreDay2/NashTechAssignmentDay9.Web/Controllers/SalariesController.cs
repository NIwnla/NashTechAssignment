using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Common.Models;
using NashTechAssignmentDay9.Application.Models;

namespace NashTechAssignmentDay9.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalariesController : ControllerBase
{
    private readonly ISalaryService _salaryService;
    public SalariesController(ISalaryService salaryService)
    {
        _salaryService = salaryService;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var salarys = _salaryService.GetAll();
        if (salarys.Count() <= 0 || salarys == null)
        {
            return NotFound();
        }
        return Ok(salarys);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] Guid? employeeId, [FromBody] SalaryDto salaryDto)
    {
        if (employeeId == null)
        {
            ModelState.AddModelError("", "Require Id");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _salaryService.CreateAsync(employeeId.Value, salaryDto))
        {
            ModelState.AddModelError("", "Something went wrong while creating salary.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Salary created.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid? id, [FromBody] SalaryDto salaryDto)
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
        if (!await _salaryService.UpdateAsync(id.Value, salaryDto))
        {
            ModelState.AddModelError("", "Something went wrong while updating salary.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Salary updated.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid? id)
    {
        if (!ModelState.IsValid || id == null)
        {
            return BadRequest(ModelState);
        }
        if (!await _salaryService.DeleteAsync(id.Value))
        {
            ModelState.AddModelError("", "Something went wrong while deleting salary.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Salary deleted.");
        }
    }

}
