namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication
{
    public interface ICompanyProfileEntity
    {
        string Name { get; set; }
    }

    public class CompanySignInEntity : IUserSignInEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class CompanySignUpEntity : ICompanyProfileEntity, IUserSignUpEntity
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class CompanyProfileEntity : ICompanyProfileEntity, IUserProfileEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
    }
}