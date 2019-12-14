using System;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile
{
    public class ClientProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? Birthday { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}