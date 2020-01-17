using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface IAppointmentStore
    {
        Task<IEnumerable<AppointmentShowItemEntity>> GetListByClient(IOperation operation, int userId);
        Task<IEnumerable<AppointmentShowItemEntity>> GetListByCarWash(IOperation operation, int carWashId);
        Task<AppointmentShowFullItemEntity> Get(IOperation operation, int appointmentId);
        Task<bool> IsExist(IOperation operation, int appointmentId);
        Task<int> Add(IOperation operation, AppointmentManageItemEntity entity);
        Task UpdateAsClient(IOperation operation, AppointmentManageItemEntity entity);
        Task UpdateAsCompany(IOperation operation, AppointmentManageItemEntity entity);
        Task ChangeStatus(IOperation operation, int appointmentId, AppointmentStatus status);
        Task<IEnumerable<CarWashServiceEntity>> GetServicesByAppointment(IOperation operation, int appointmentId);
        Task<IEnumerable<AppointmentHistoryEntity>> GetHistoryRecords(IOperation operation, int appointmentId);
        Task AddHistoryRecord(IOperation operation, int appointmentId, string action);
    }
}