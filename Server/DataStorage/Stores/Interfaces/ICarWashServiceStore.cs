using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICarWashServiceStore
    {
        Task<IEnumerable<CarWashServiceEntity>> GetListByCarWash(IOperation operation, int carWashId);
        Task<bool> IsExist(IOperation operation, int id);
        Task<CarWashServiceShortEntity> Add(IOperation operation, int carWashId, CarWashServiceEntity entity);
        Task<CarWashServiceShortEntity> Update(IOperation operation, CarWashServiceEntity entity);
        Task<CarWashServiceShortEntity> Delete(IOperation operation, int id);
    }
}