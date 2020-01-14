using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<CarBrandModelsEntity>> GetCarBrandModels(IOperation operation);
        IEnumerable<EnumRowEntity> GetAppointmentStatuses();
    }
}