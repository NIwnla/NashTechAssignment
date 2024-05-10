using Microsoft.AspNetCore.Mvc;

namespace NashTechAssignmentDay8.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("This work :)");
    }
}
