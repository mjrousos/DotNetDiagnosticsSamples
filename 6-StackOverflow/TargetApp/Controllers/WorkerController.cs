using Microsoft.AspNetCore.Mvc;

namespace TargetApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController : ControllerBase
{
    private readonly ILogger<WorkerController> _logger;
    private GreatestCommonDivisorBad _gcdService = new GreatestCommonDivisorBad();

    public WorkerController(ILogger<WorkerController> logger)
    {
        _logger = logger;
    }

    [HttpGet("RunTask/{x}/{y}")]
    public ActionResult<long> Get(long x, long y)
    {
        var ret = _gcdService.GCD(x, y);
        _logger.LogInformation("Calculated that the greatest common divisor of {X} and {Y} is {Result}", x, y, ret);
        return Ok(ret);
    }
}
