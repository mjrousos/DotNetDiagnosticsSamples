using System;
using System.Runtime.Caching;

namespace TargetApp
{
    public class ProfilePictureCacheGood
    {
        public ProfilePictureCacheGood()
        {
        }

        public byte[] GetOrAdd(Guid id, Func<Guid, byte[]> dataGenerator)
        {
            var cache = MemoryCache.Default;

            if (!(cache[id.ToString()] is byte[] bytes))
            {
                bytes = dataGenerator(id);

                var itemPolicy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(5) };
                cache.Set(id.ToString(), bytes, itemPolicy);
            }

            return bytes;
        }
    }
}
