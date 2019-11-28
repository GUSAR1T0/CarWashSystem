namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication
{
    public interface IClientProfileEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class ClientSignInEntity : IUserSignInEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ClientSignUpEntity : IClientProfileEntity, IUserSignUpEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public class ExternalClientSignInEntity : IClientProfileEntity
    {
        public string? ExternalId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public ExternalUserAuthenticationSchema? Schema { get; set; }
    }

    public class ClientProfileEntity : IClientProfileEntity, IUserProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}