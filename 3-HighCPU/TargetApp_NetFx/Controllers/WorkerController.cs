using System;
using System.Web.Http;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    public class WorkerController : ApiController
    {
        private readonly BadWorker _workerService;

        public WorkerController(BadWorker workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public IHttpActionResult RunTask(int duration)
        {
            _workerService.DoWork(duration);
            return Ok();
        }
    }
}
