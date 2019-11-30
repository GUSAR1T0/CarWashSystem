namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile
{
    public class CarWashFullEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Location { get; set; } = "";
        public bool HasCafe { get; set; }
        public bool HasRestZone { get; set; }
        public bool HasParking { get; set; }
        public bool HasWC { get; set; }
        public bool HasCardPayment { get; set; }
    }

    public class CarWashShortEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}