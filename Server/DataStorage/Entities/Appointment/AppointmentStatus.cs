using System.ComponentModel;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment
{
    public enum AppointmentStatus : byte
    {
        [Description("Opened")]
        Opened = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Response Is Required")]
        ResponseIsRequired = 3,

        [Description("In Progress")]
        InProgress = 4,

        [Description("Processed")]
        Processed = 5,

        [Description("Incident")]
        Incident = 6,

        [Description("Closed")]
        Closed = 7,

        [Description("Cancelled By Client")]
        CancelledByClient = 8,

        [Description("Cancelled By Car Wash")]
        CancelledByCarWash = 9
    }
}