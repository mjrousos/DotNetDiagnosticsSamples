using System;

namespace TargetApp.Services
{
    public abstract class ProfilePictureService
    {
        protected abstract ProfilePicture CreateProfilePicture(int size);

        public ProfilePicture LookupProfilePicture(Guid id)
        {
            // This would normally retrieve a profile picture from storage
            // For a simpler demo, this will just get 100k random bytes, instead.
            var ret = CreateProfilePicture(100 * 1024);
            //var random = new Random();
            //random.NextBytes(ret.Bytes);
            return ret;
        }
    }
}
