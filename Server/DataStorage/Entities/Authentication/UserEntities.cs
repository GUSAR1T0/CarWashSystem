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
    }
}