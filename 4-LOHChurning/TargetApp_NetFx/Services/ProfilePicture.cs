using System;

namespace TargetApp.Services
{
    public abstract class ProfilePicture : IDisposable
    {
        public byte[] Bytes { protected set; get; }

        public abstract void Dispose();
    }
}
