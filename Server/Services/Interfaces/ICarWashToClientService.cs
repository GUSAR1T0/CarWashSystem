using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface ICarWashToClientService
    {
        Task<IEnumerable<CarWashFullEntity>> GetCarWashList(IOperation operation);
        Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id);
        Task<IEnumerable<CarWashServiceEntity>> GetCarWashServiceListByCarWash(IOperation operation, int carWashId);
    }
}