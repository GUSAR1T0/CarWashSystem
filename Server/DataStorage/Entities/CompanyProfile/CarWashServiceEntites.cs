using System;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile
{
    public class CarWashServiceEntity
    {
        public int Id { get; set; }
        public int CarWashId { get; set; }
        public string ServiceName { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class CarWashServiceShortEntity
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = "";
    }
}