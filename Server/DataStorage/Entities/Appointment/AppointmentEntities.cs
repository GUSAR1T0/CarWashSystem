using System;
using System.Collections.Generic;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment
{
    public class AppointmentShowItemEntity
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }
        public string CarGovernmentPlate { get; set; } = "";
        public string CarWashName { get; set; } = "";
        public string CarWashLocation { get; set; } = "";
        public DateTime RequestedStartTime { get; set; }
        public DateTime? ApprovedStartTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public IEnumerable<CarWashServiceEntity> CarWashServices { get; set; } = new List<CarWashServiceEntity>();
    }

    public class AppointmentShowItemWithHistoryEntity : AppointmentShowItemEntity
    {
        public IEnumerable<AppointmentHistoryEntity> History { get; set; } = new List<AppointmentHistoryEntity>();
    }

    public class AppointmentManageItemEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CarWashId { get; set; }
        public DateTime StartTime { get; set; }
        public IEnumerable<int> CarWashServiceIds { get; set; } = new List<int>();
    }
}