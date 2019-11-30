namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile
{
    public class CompanyProfileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Phone { get; set; }
    }
}