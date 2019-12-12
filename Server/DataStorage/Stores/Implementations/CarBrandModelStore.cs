using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarBrandModelStore : ICarBrandModelStore
    {
        public async Task<IEnumerable<CarBrandEntity>> GetCarBrands(IOperation operation)
        {
            return await operation.QueryAsync<CarBrandEntity>(@"
                SELECT
                    [Id],
                    [Name]
                FROM [client].[CarBrandEnum];
            ");
        }

        public async Task<IEnumerable<CarBrandModelEntity>> GetCarBrandModels(IOperation operation)
        {
            return await operation.QueryAsync<CarBrandModelEntity>(@"
                SELECT
                    [Id],
                    [BrandId],
                    [Name]
                FROM [client].[CarBrandModelEnum];
            ");
        }
    }
}