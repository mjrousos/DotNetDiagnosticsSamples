using Microsoft.AspNetCore.Mvc;

namespace TargetApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerFixedController : ControllerBase
{
    private readonly ILogger<WorkerFixedController> _logger;
    private GreatestCommonDivisorGood _gcdService = new GreatestCommonDivisorGood();

    public WorkerFixedController(ILogger<WorkerFixedController> logger)
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
