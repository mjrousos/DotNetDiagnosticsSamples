namespace TargetApp.Services
{
    public class ProfilePictureServiceBad : ProfilePictureService
    {
        protected override ProfilePicture CreateProfilePicture(int size) => new ProfilePictureBad(size);
    }
}
