using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICarWashStore
    {
        Task<IEnumerable<CarWashShortEntity>> GetAllShortByCompanyId(IOperation operation, int companyId);
        Task<IEnumerable<CarWashFullEntity>> GetAllFullByCompanyId(IOperation operation, int companyId);
        Task<bool> IsExist(IOperation operation, int id);
        Task<CarWashFullEntity> GetById(IOperation operation, int id);
        Task<CarWashShortEntity> Add(IOperation operation, int companyId, CarWashFullEntity entity);
        Task<CarWashShortEntity> Update(IOperation operation, CarWashFullEntity entity);
        Task<CarWashShortEntity> Delete(IOperation operation, int id);
    }
}