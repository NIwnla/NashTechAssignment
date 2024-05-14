using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var projects = _projectService.GetAll();
        if (projects.Count() <= 0 || projects == null)
        {
            return NotFound();
        }
        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectDto projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _projectService.CreateAsync(projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while creating project.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Project {projectDto.Name} created.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid? id, [FromBody] ProjectDto projectDto)
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
        if (!await _projectService.UpdateAsync(id.Value, projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while updating project.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Project updated.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid? id)
    {
        if (!ModelState.IsValid || id == null)
        {
            return BadRequest(ModelState);
        }
        if (!await _projectService.DeleteAsync(id.Value))
        {
            ModelState.AddModelError("", "Something went wrong while deleting project.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        else
        {
            return Ok($"Project deleted.");
        }
    }

}
