using System.Web.Http;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private GreatestCommonDivisorGood _gcdService = new GreatestCommonDivisorGood();

        public WorkerFixedController()
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