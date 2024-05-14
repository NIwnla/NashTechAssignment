using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay6.Application.Common.Interfaces;
using NashTechAssignmentDay6.Domain.Entities;

namespace NashTechAssignmentDay6.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkTaskController : ControllerBase
	{
		private readonly IWorkTaskService _workTaskService;
		public WorkTaskController(IWorkTaskService workTaskService)
		{
			_workTaskService = workTaskService;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var workTasks = _workTaskService.GetAllWorkTasks();
			if(workTasks == null)
			{
				return NotFound("No task found");
			}
			return Ok(workTasks);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id) 
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var workTask = _workTaskService.GetWorkTaskById(id);
			if(workTask == null)
			{
				return NotFound("No task found");
			}
			return Ok(workTask);
		}

		[HttpDelete]
		public IActionResult Delete([FromBody]WorkTask workTask) 
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if(_workTaskService.Remove(workTask))
			{
				return Ok("task deleted");
			}
			ModelState.AddModelError("", "Something went wrong while deleting task");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}


		[HttpDelete("DeleteRange")]
		public IActionResult DeleteRange([FromBody] IEnumerable<WorkTask> workTasks)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_workTaskService.RemoveRange(workTasks))
			{
				return Ok("tasks deleted");
			}
			ModelState.AddModelError("", "Something went wrong while deleting tasks");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPut]
		public IActionResult Edit([FromBody] WorkTask workTask)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if(_workTaskService.Update(workTask))
			{
				return Ok("Task updated");
			}
			ModelState.AddModelError("", "Something went wrong while updating task");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPost]
		public IActionResult Create([FromBody]WorkTask workTask)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_workTaskService.Create(workTask))
			{
				return Ok("Task created");
			}
			ModelState.AddModelError("", "Something went wrong while creating task");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPost("CreateRange")]
		public IActionResult CreateRange([FromBody] IEnumerable<WorkTask> workTasks)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_workTaskService.CreateRange(workTasks))
			{
				return Ok("Tasks created");
			}
			ModelState.AddModelError("", "Something went wrong while creating tasks");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
	}
}
