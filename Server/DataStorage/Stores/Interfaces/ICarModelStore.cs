using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICarBrandModelStore
    {
        Task<IEnumerable<CarBrandEntity>> GetCarBrands(IOperation operation);
        Task<IEnumerable<CarBrandModelEntity>> GetCarBrandModels(IOperation operation);
    }
}