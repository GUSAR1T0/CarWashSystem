using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICarWashServicePriceStore
    {
        Task<IEnumerable<CarWashServicePriceEntity>> GetListByCompany(IOperation operation, int userId);
        Task<IEnumerable<CarWashServicePriceEntity>> GetListByCarWash(IOperation operation, int carWashId);
        Task<bool> IsExist(IOperation operation, int id);
        Task<CarWashServicePriceShortEntity> Add(IOperation operation, int carWashId, CarWashServicePriceEntity entity);
        Task<CarWashServicePriceShortEntity> Update(IOperation operation, CarWashServicePriceEntity entity);
        Task<CarWashServicePriceShortEntity> Delete(IOperation operation, int id);
    }
}