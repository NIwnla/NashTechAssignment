using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var employees = _employeeService.GetAll();
        if (employees.Count() <= 0 || employees == null)
        {
            return NotFound();
        }
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] Guid? departmentId, [FromBody] EmployeeDto employeeDto)
    {
        if (departmentId == null)
        {
            ModelState.AddModelError("", "Require Id");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _employeeService.CreateAsync(departmentId.Value, employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while creating employee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Employee {employeeDto.Name} created.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid? id, [FromBody] EmployeeDto employeeDto)
    {
        if(id == null){
            ModelState.AddModelError("","Require Id");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _employeeService.UpdateAsync(id.Value, employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while updating employee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Employee updated.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid? id)
    {
        if (!ModelState.IsValid || id == null)
        {
            return BadRequest(ModelState);
        }
        if (!await _employeeService.DeleteAsync(id.Value))
        {
            ModelState.AddModelError("", "Something went wrong while deleting employee.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Employee deleted.");
        }
    }

    [HttpGet("Departments")]
    public IActionResult SpecialQuery1(){
        var result = _employeeService.GetAllWithDepartment();
        return Ok(result);
    }
    [HttpGet("Projects")]
    public IActionResult SpecialQuery2()
    {
        var result = _employeeService.GetAllWithProjects();
        return Ok(result);
    }
    [HttpGet("Salaries")]
    public IActionResult SpecialQuery3()
    {
        var result = _employeeService.GetAllWithSalary();
        return Ok(result);
    }
}
