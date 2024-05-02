using Microsoft.AspNetCore.Mvc;

namespace NashTechAssignmentDay3.Controller
{
	public class HomeController : ControllerBase
	{
		[HttpPost("Test")]
		public IActionResult OnPost([FromBody] string name,[FromQuery] string age)
		{
			return Ok(new {Name = name, Age = age});
		}
	}
}
