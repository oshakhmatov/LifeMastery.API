using Microsoft.AspNetCore.Mvc;

namespace LifeMastery.API.Controllers;

[Route("api/healthcheck")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok("Service is running");
    }
}