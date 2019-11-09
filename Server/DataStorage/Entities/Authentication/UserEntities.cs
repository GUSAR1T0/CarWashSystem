namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication
{
    public interface IUserSignInEntity
    {
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface IUserSignUpEntity
    {
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface IUserProfileEntity
    {
        int Id { get; set; }
        string Email { get; set; }
    }
    
    public class UserProfileEntity : IUserProfileEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
    }
}