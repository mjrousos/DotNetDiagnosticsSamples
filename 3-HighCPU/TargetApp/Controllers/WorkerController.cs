using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly BadWorker _workerService;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(BadWorker workerService, ILogger<WorkerController> logger)
        {
            _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("RunTask/{duration}")]
        public ActionResult RunTask(int duration)
        {
            _workerService.DoWork(duration);
            return Ok();
        }
    }
}
