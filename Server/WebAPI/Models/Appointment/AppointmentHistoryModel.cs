using System;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Appointment
{
    public class AppointmentHistoryModel : IEntityToModelConvertible<AppointmentHistoryEntity, AppointmentHistoryModel>
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Action { get; set; } = "";
        public string Timestamp { get; set; } = "";

        public AppointmentHistoryModel ToModel(AppointmentHistoryEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            AppointmentId = entity.AppointmentId;
            Action = entity.Action;
            Timestamp = entity.Timestamp.ToLocalTime().ToString(Formatters.HistoryTimeFormat);
            return this;
        }
    }
}