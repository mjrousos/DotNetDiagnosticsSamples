using System;
using System.Web.Http;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private readonly ProfilePictureService _profilePictureService;
        private readonly NativeProfilePictureService _nativeProfilePictureService;

        public WorkerFixedController(ProfilePictureService profilePictureService, NativeProfilePictureService nativeProfilePictureService)
        {
            _profilePictureService = profilePictureService ?? throw new ArgumentNullException(nameof(profilePictureService));
            _nativeProfilePictureService = nativeProfilePictureService ?? throw new ArgumentNullException(nameof(nativeProfilePictureService));
        }

        [HttpGet]
        public IHttpActionResult RunTask()
        {
            // Get a random ID (normally this would be provided by the caller)
            var id = Guid.NewGuid();
            var pictureBytes = _profilePictureService.GetProfilePictureGood(id);

            if (pictureBytes is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pictureBytes);
            }
        }

        [HttpGet]
        public IHttpActionResult RunTaskNative()
        {
            // Get a random ID (normally this would be provided by the caller)
            var id = Guid.NewGuid();
            var pictureBytes = _nativeProfilePictureService.GetProfilePictureGood(id);

            if (pictureBytes is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pictureBytes);
            }
        }
    }
}
