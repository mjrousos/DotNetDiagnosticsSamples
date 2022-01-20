using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Http;
using TargetApp.Services;

namespace TargetApp.Controllers
{
    public class WorkerFixedController : ApiController
    {
        private readonly ProfilePictureService _profilePictureService;
        private readonly HashAlgorithm _hashAlgorithm = SHA256.Create();

        public WorkerFixedController(ProfilePictureServiceGood profilePictureService)
        {
            _profilePictureService = profilePictureService ?? throw new ArgumentNullException(nameof(profilePictureService));
        }

        [HttpGet]
        public IHttpActionResult RunTask()
        {
            // Create a random collection of guids (normally this would be some IDs queried from a database or something like that)
            var guids = Enumerable.Range(0, 30).Select(_ => Guid.NewGuid());

            var ret = new List<string>();
            foreach (var id in guids)
            {
                using (var pictureBytes = _profilePictureService.LookupProfilePicture(id))
                {
                    if (pictureBytes != null)
                    {
                        // Commented out so that the CPU usage of the hashing doesn't make
                        // expected GC-related issues harder to spot.
                        //var hashBytes = _hashAlgorithm.ComputeHash(pictureBytes.Bytes);
                        //ret.Add(Convert.ToBase64String(hashBytes));
                    }
                }
            }

            return Ok(ret);
        }
    }
}