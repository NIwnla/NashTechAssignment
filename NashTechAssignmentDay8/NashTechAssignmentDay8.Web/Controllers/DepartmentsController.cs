using Microsoft.AspNetCore.Mvc;

namespace NashTechAssignmentDay8.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return NoContent();
    }
}
