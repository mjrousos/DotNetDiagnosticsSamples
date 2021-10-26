using Microsoft.AspNetCore.Mvc;

namespace TargetApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController : ControllerBase
{
    private static readonly SemaphoreSlim _s1 = new(1, 1);
    private static readonly SemaphoreSlim _s2 = new(1, 1);

    private readonly ILogger<WorkerController> _logger;

    public WorkerController(ILogger<WorkerController> logger)
    {
        _logger = logger;
    }

    [HttpGet("RunTask")]
    public async Task<ActionResult<string>> RunTask()
    {
        await _s1.WaitAsync();
        try
        {
            var retPart1 = await HelperAsync();
            await _s2.WaitAsync();
            try
            {

                var retPart2 = "World";
                return $"{retPart1} {retPart2}";
            }
            finally
            {
                _s2.Release();
            }
        }
        finally
        {
            _s1.Release();
        }
    }

    private static async Task<string> HelperAsync()
    {
        string? ret;
        await _s2.WaitAsync();
        try
        {
            await _s1.WaitAsync();

            try
            {
                ret = "Hello";
                return ret;
            }
            finally
            {
                _s1.Release();
            }
        }
        finally
        {
            _s2.Release();
        }
    }
}
