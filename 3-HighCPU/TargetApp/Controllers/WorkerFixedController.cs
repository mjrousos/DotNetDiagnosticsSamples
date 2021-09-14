using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerFixedController : ControllerBase
    {
        private readonly GoodWorker _workerService;
        private readonly ILogger<WorkerController> _logger;

        public WorkerFixedController(GoodWorker workerService, ILogger<WorkerController> logger)
        {
            _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("RunTask/{duration}")]
        public async Task<ActionResult> RunTask(int duration)
        {
            await _workerService.DoWorkAsync(duration);
            return Ok();
        }
    }
}
