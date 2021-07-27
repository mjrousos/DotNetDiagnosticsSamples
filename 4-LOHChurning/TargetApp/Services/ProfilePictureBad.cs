namespace TargetApp.Services
{
    public class ProfilePictureBad : ProfilePicture
    {
        public ProfilePictureBad(int size)
        {
            Bytes = new byte[size];
        }

        public override void Dispose() { }
    }
}
