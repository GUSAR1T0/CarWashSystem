using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentShowItemEntity>> GetListOfAppointmentsByClient(IOperation operation, int userId);
        Task<IEnumerable<AppointmentShowItemEntity>> GetListOfAppointmentsByCarWash(IOperation operation, int carWashId);
        Task<AppointmentShowItemWithHistoryEntity> GetAppointment(IOperation operation, int appointmentId);
        Task AddAppointment(IOperation operation, AppointmentManageItemEntity entity);
        Task UpdateAppointment(IOperation operation, UserRole role, AppointmentManageItemEntity entity);
        Task ChangeAppointmentStatus(IOperation operation, UserRole role, int appointmentId, AppointmentStatus status, string? comment = null);
    }
}