namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication
{
    public interface IClientAuthenticationProfileEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class ClientSignInEntity : IUserSignInEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ClientSignUpEntity : IClientAuthenticationProfileEntity, IUserSignUpEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public class ExternalClientSignInEntity : IClientAuthenticationProfileEntity
    {
        public string? ExternalId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public ExternalUserAuthenticationSchema? Schema { get; set; }
    }

    public class ClientAuthenticationProfileEntity : IClientAuthenticationProfileEntity, IUserAuthenticationProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}