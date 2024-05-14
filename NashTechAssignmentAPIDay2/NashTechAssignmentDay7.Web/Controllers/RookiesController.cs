using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashTechAssignmentDay7.Application.Common.Interfaces;
using NashTechAssignmentDay7.Domain.Entities;

namespace NashTechAssignmentDay7.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RookiesController : ControllerBase
	{
		private readonly IRookiesService _rookiesService;
		public RookiesController(IRookiesService rookiesService)
		{
			_rookiesService = rookiesService;
		}

		[HttpGet]
		public IActionResult GetRookies([FromQuery] string? firstName, string? lastName, string? gender, string? birthPlace)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var rookies = _rookiesService.GetPeopleByFilter(firstName, lastName, gender, birthPlace);
			if (rookies == null || rookies.Count() <= 0)
			{
				return NotFound("No rookie satisfied your filter");
			}
			return Ok(rookies);
		}

		[HttpDelete]
		public IActionResult Delete([FromBody] Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_rookiesService.Remove(person))
			{
				return Ok("rookie deleted");
			}
			ModelState.AddModelError("", "Something went wrong while deleting rookie");
			return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
		}

		[HttpPut]
		public IActionResult Edit([FromBody] Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_rookiesService.Update(person))
			{
				return Ok("Rookie updated");
			}
			ModelState.AddModelError("", "Something went wrong while updating rookie");
			return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (_rookiesService.Create(person))
			{
				return Ok("Rookie created");
			}
			ModelState.AddModelError("", "Something went wrong while creating rookie");
			return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
		}
	}
}
