namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication
{
    public interface ICompanyAuthenticationProfileEntity
    {
        string Name { get; set; }
    }

    public class CompanySignInEntity : IUserSignInEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class CompanySignUpEntity : ICompanyAuthenticationProfileEntity, IUserSignUpEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class CompanyAuthenticationProfileEntity : ICompanyAuthenticationProfileEntity, IUserAuthenticationProfileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}