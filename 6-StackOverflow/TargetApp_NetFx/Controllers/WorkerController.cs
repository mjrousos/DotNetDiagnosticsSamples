using System.Web.Http;

namespace TargetApp.Controllers
{
    public class WorkerController : ApiController
    {
        private GreatestCommonDivisorBad _gcdService = new GreatestCommonDivisorBad();

        public WorkerController()
        {
        }

        [HttpGet]
        public IHttpActionResult RunTask(long x, long y)
        {
            var ret = _gcdService.GCD(x, y);
            return Ok(ret);
        }
    }
}