using System.Buffers;

namespace TargetApp.Services
{
    public class ProfilePictureGood : ProfilePicture
    {
        public ProfilePictureGood(int size)
        {
            Bytes = ArrayPool<byte>.Shared.Rent(size);
        }

        public override void Dispose()
        {
            ArrayPool<byte>.Shared.Return(Bytes);
        }
    }
}
