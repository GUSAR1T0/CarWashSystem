using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarWashServiceStore : ICarWashServiceStore
    {
        public Task<IEnumerable<CarWashServiceEntity>> GetListByCarWash(IOperation operation, int carWashId)
        {
            return operation.QueryAsync<CarWashServiceEntity>(new
            {
                CarWashId = carWashId
            }, @"
                SELECT
                    ccws.[Id],
                    ccws.[CarWashId],
                    ccws.[ServiceName],
                    ccws.[Description],
                    ccws.[Price],
                    ccws.[Duration],
                    ccws.[IsAvailable]
                FROM [company].[CarWashService] ccws
                WHERE ccws.[CarWashId] = @CarWashId;
            ");
        }

        public Task<bool> IsExist(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [company].[CarWashService]
                WHERE [Id] = @Id;
            ");
        }

        public Task<CarWashServiceShortEntity> Add(IOperation operation, int carWashId, CarWashServiceEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServiceShortEntity>(new
            {
                CarWashId = carWashId,
                entity.ServiceName,
                entity.Description,
                entity.Price,
                entity.Duration,
                entity.IsAvailable
            }, @"
                DECLARE @NewCarWashService TABLE ([Id] INT, [ServiceName] NVARCHAR (50));

                INSERT INTO [company].[CarWashService] (
                    [CarWashId],
                    [ServiceName],
                    [Description],
                    [Price],
                    [Duration],
                    [IsAvailable]
                )
                OUTPUT INSERTED.[Id], INSERTED.[ServiceName] INTO @NewCarWashService
                VALUES (
                    @CarWashId,
                    @ServiceName,
                    @Description,
                    @Price,
                    @Duration,
                    @IsAvailable
                )

                SELECT
                    [Id],
                    [ServiceName]
                FROM @NewCarWashService;
            ");
        }

        public Task<CarWashServiceShortEntity> Update(IOperation operation, CarWashServiceEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServiceShortEntity>(entity, @"
                DECLARE @UpdatedCarWashService TABLE ([Id] INT, [ServiceName] NVARCHAR (50));

                UPDATE [company].[CarWashService]
                SET
                    [ServiceName] = @ServiceName,
                    [Description] = @Description,
                    [Price] = @Price,
                    [Duration] = @Duration,
                    [IsAvailable] = @IsAvailable
                OUTPUT INSERTED.[Id], INSERTED.[ServiceName] INTO @UpdatedCarWashService
                WHERE [Id] = @Id

                SELECT
                    [Id],
                    [ServiceName]
                FROM @UpdatedCarWashService;
            ");
        }

        public Task<CarWashServiceShortEntity> Delete(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServiceShortEntity>(new
            {
                Id = id
            }, @"
                DECLARE @DeletedCarWashService TABLE ([Id] INT, [ServiceName] NVARCHAR (50));

                DELETE [company].[CarWashService]
                OUTPUT DELETED.[Id], DELETED.[ServiceName] INTO @DeletedCarWashService
                FROM [company].[CarWashService]
                WHERE [Id] = @Id;

                SELECT
                    [Id],
                    [ServiceName]
                FROM @DeletedCarWashService;
            ");
        }
    }
}