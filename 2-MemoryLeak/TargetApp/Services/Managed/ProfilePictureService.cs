using System;

namespace TargetApp
{
    public class ProfilePictureService
    {
        private readonly ProfilePictureCacheBad _badCache;
        private readonly ProfilePictureCacheGood _goodCache;

        public ProfilePictureService(ProfilePictureCacheBad badCache, ProfilePictureCacheGood goodCache)
        {
            _badCache = badCache ?? throw new ArgumentNullException(nameof(badCache));
            _goodCache = goodCache ?? throw new ArgumentNullException(nameof(goodCache));
        }

        public byte[] GetProfilePictureBad(Guid id) => _badCache.GetOrAdd(id, LookupProfilePicture);

        public byte[] GetProfilePictureGood(Guid id) => _goodCache.GetOrAdd(id, LookupProfilePicture);

        private byte[] LookupProfilePicture(Guid id)
        {
            // This would normally retrieve a profile picture from storage
            // For a simpler demo, this will just get 100k random bytes, instead.
            var ret = new byte[100 * 1024];
            var random = new Random();
            random.NextBytes(ret);
            return ret;
        }
    }
}
