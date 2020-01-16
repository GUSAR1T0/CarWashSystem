using System;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment
{
    public class AppointmentHistoryEntity
    {
        public int AppointmentId { get; set; }
        public string Action { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }
}