using System;
using System.Threading.Tasks;
using System.Web.Http;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private readonly GoodWorker _workerService;

        public WorkerFixedController(GoodWorker workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> RunTask(int duration)
        {
            await _workerService.DoWorkAsync(duration);
            return Ok();
        }
    }
}
