namespace TargetApp.Services
{
    public class ProfilePictureServiceGood : ProfilePictureService
    {
        protected override ProfilePicture CreateProfilePicture(int size) => new ProfilePictureGood(size);
    }
}
