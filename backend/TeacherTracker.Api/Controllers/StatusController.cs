using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherTracker.Api.Controllers
{
    [ApiController]
    [Route("/api/status")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                Application = "TeacherTracker",
                Status = "Running"
            });
        }
    }
}
