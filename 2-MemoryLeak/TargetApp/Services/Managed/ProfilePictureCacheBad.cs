using System;
using System.Collections.Concurrent;

namespace TargetApp
{
    /// <summary>
    /// This is a badly implemented cache. Do not cache like this.
    /// </summary>
    public class ProfilePictureCacheBad
    {
        private ConcurrentDictionary<Guid, byte[]> _cache;

        public ProfilePictureCacheBad()
        {
            _cache = new ConcurrentDictionary<Guid, byte[]>();
        }

        public byte[] GetOrAdd(Guid id, Func<Guid, byte[]> dataGenerator) => _cache.GetOrAdd(id, dataGenerator);
    }
}
