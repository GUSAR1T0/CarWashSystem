namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities
{
    public class CompanySignInEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class CompanySignUpEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class CompanyProfileEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}