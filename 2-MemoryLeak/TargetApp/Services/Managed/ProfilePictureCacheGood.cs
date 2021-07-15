using Microsoft.Extensions.Caching.Memory;
using System;

namespace TargetApp
{
    public class ProfilePictureCacheGood
    {
        private IMemoryCache _cache;

        public ProfilePictureCacheGood(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public byte[] GetOrAdd(Guid id, Func<Guid, byte[]> dataGenerator)
        {
            if (_cache.TryGetValue(id, out byte[] bytes))
            {
                bytes = dataGenerator(id);

                // Limit the cache entry's lifetime by both size and timespan
                var options = new MemoryCacheEntryOptions()
                    .SetSize(bytes.Length)
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(id, bytes, options);
            }

            return bytes;
        }
    }
}
