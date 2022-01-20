using System;
using System.Collections.Generic;

namespace TargetApp
{
    public class NativeProfilePictureService
    {
        public IEnumerable<byte> GetProfilePictureBad(Guid id) => new NativeProfilePictureBad(id);

        public IEnumerable<byte> GetProfilePictureGood(Guid id) => new NativeProfilePictureGood(id);
    }
}
