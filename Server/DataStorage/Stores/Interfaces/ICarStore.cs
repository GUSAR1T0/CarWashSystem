using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICarStore
    {
        Task<IEnumerable<CarEntity>> GetAllByUserId(IOperation operation, int userId);
        Task<bool> IsExist(IOperation operation, int id);
        Task<CarEntity> GetById(IOperation operation, int id);
        Task Add(IOperation operation, int userId, CarEntity entity);
        Task Update(IOperation operation, CarEntity entity);
        Task Delete(IOperation operation, int id);
    }
}