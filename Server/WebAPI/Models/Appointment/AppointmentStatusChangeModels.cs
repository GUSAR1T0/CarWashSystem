using System;
using System.ComponentModel.DataAnnotations;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Appointment
{
    public class AppointmentToResponseIsRequiredStatusModel
    {
        [Required]
        public DateTime StartTime { get; set; }
    }

    public class AppointmentToCancelStatusModel
    {
        public string? Comment { get; set; }
    }
}