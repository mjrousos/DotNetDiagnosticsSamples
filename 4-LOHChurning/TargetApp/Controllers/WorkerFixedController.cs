using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerFixedController : ControllerBase
    {
        private readonly ProfilePictureService _profilePictureService;
        private readonly ILogger<WorkerController> _logger;
        private readonly HashAlgorithm _hashAlgorithm = SHA256.Create();

        public WorkerFixedController(ProfilePictureServiceGood profilePictureService, ILogger<WorkerController> logger)
        {
            _profilePictureService = profilePictureService ?? throw new ArgumentNullException(nameof(profilePictureService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("RunTask")]
        public ActionResult<string> RunTask()
        {
            // Create a random collection of guids (normally this would be some IDs queried from a database or something like that)
            var guids = Enumerable.Range(0, 30).Select(_ => Guid.NewGuid());

            var ret = new List<string>();
            foreach (var id in guids)
            {
                using var pictureBytes = _profilePictureService.LookupProfilePicture(id);
                if (pictureBytes != null)
                {
                    //var hashBytes = _hashAlgorithm.ComputeHash(pictureBytes.Bytes);
                    //ret.Add(Convert.ToBase64String(hashBytes));
                }
            }

            return Ok(ret);
        }
    }
}
