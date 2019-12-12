using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarStore : ICarStore
    {
        public async Task<IEnumerable<CarEntity>> GetAllByUserId(IOperation operation, int userId)
        {
            return await operation.QueryAsync<CarEntity>(new
            {
                UserId = userId
            }, @"
                SELECT
                    cca.[Id],
                    cca.[ModelId],
                    (cbe.[Name] + ' ' + cme.[Name]) AS [Model],
                    cca.[GovernmentPlate]
                FROM [client].[Car] cca
                INNER JOIN [client].[Client] cc ON cca.[ClientId] = cc.[Id]
                INNER JOIN [client].[CarBrandModelEnum] cme ON cca.[ModelId] = cme.[Id]
                INNER JOIN [client].[CarBrandEnum] cbe ON cme.[BrandId] = cbe.[Id]
                WHERE cc.[UserId] = @UserId;
            ");
        }

        public async Task<bool> IsExist(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [client].[Car]
                WHERE [Id] = @Id;
            ");
        }

        public async Task<CarEntity> GetById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CarEntity>(new
            {
                Id = id
            }, @"
                SELECT
                    cca.[Id],
                    cca.[ModelId],
                    (cbe.[Name] + ' ' + cme.[Name]) AS [Model],
                    cca.[GovernmentPlate]
                FROM [client].[Car] cca
                INNER JOIN [client].[Client] cc ON cca.[ClientId] = cc.[Id]
                INNER JOIN [client].[CarBrandModelEnum] cme ON cca.[ModelId] = cme.[Id]
                INNER JOIN [client].[CarBrandEnum] cbe ON cme.[BrandId] = cbe.[Id]
                WHERE cca.[Id] = @Id;
            ");
        }

        public async Task Add(IOperation operation, int userId, CarEntity entity)
        {
            await operation.ExecuteAsync(new
            {
                UserId = userId,
                entity.ModelId,
                entity.GovernmentPlate
            }, @"
                INSERT INTO [client].[Car] (
                    [ClientId],
                    [ModelId],
                    [GovernmentPlate]
                )
                SELECT
                    [Id],
                    @ModelId,
                    @GovernmentPlate
                FROM [client].[Client]
                WHERE [UserId] = @UserId;
            ");
        }

        public async Task Update(IOperation operation, CarEntity entity)
        {
            await operation.ExecuteAsync(entity, @"
                UPDATE [client].[Car]
                SET
                    [ModelId] = @ModelId,
                    [GovernmentPlate] = @GovernmentPlate
                WHERE [Id] = @Id;
            ");
        }

        public async Task Delete(IOperation operation, int id)
        {
            await operation.ExecuteAsync(new
            {
                Id = id
            }, @"
                DELETE
                FROM [client].[Car]
                WHERE [Id] = @Id;
            ");
        }
    }
}