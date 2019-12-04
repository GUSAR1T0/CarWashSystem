using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarWashServicePriceStore : ICarWashServicePriceStore
    {
        public Task<IEnumerable<CarWashServicePriceEntity>> GetListByCompany(IOperation operation, int userId)
        {
            return operation.QueryAsync<CarWashServicePriceEntity>(new
            {
                UserId = userId
            }, @"
                SELECT
                    ccwsp.[Id],
                    ccwsp.[CarWashId],
                    ccwsp.[ServiceName],
                    ccwsp.[Description],
                    ccwsp.[Price],
                    ccwsp.[Duration],
                    ccwsp.[IsAvailable]
                FROM [company].[CarWashServicePrice] ccwsp
                INNER JOIN [company].[CarWash] ccw ON ccwsp.[CarWashId] = ccw.[Id]
                INNER JOIN [company].[Company] cc ON ccw.[CompanyId] = cc.[Id]
                WHERE cc.[UserId] = @UserId;
            ");
        }

        public Task<IEnumerable<CarWashServicePriceEntity>> GetListByCarWash(IOperation operation, int carWashId)
        {
            return operation.QueryAsync<CarWashServicePriceEntity>(new
            {
                CarWashId = carWashId
            }, @"
                SELECT
                    ccwsp.[Id],
                    ccwsp.[CarWashId],
                    ccwsp.[ServiceName],
                    ccwsp.[Description],
                    ccwsp.[Price],
                    ccwsp.[Duration],
                    ccwsp.[IsAvailable]
                FROM [company].[CarWashServicePrice] ccwsp
                WHERE ccwsp.[CarWashId] = @CarWashId;
            ");
        }

        public Task<bool> IsExist(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [company].[CarWashServicePrice]
                WHERE [Id] = @Id;
            ");
        }

        public Task<CarWashServicePriceShortEntity> Add(IOperation operation, int carWashId, CarWashServicePriceEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServicePriceShortEntity>(new
            {
                CarWashId = carWashId,
                entity.ServiceName,
                entity.Description,
                entity.Price,
                entity.Duration,
                entity.IsAvailable
            }, @"
                DECLARE @NewCarWashServicePrice TABLE ([Id] INT, [ServiceName] NVARCHAR (50));

                INSERT INTO [company].[CarWashServicePrice] (
                    [CarWashId],
                    [ServiceName],
                    [Description],
                    [Price],
                    [Duration],
                    [IsAvailable]
                )
                OUTPUT INSERTED.[Id], INSERTED.[ServiceName] INTO @NewCarWashServicePrice
                SELECT
                    @CarWashId,
                    @ServiceName,
                    @Description,
                    @Price,
                    @Duration,
                    @IsAvailable
                FROM [company].[Company]
                WHERE [UserId] = @UserId;

                SELECT
                    [Id],
                    [ServiceName]
                FROM @NewCarWashServicePrice;
            ");
        }

        public Task<CarWashServicePriceShortEntity> Update(IOperation operation, CarWashServicePriceEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServicePriceShortEntity>(entity, @"
                DECLARE @UpdatedCarWash TABLE ([Id] INT, [ServiceName] NVARCHAR (50));

                UPDATE [company].[CarWashServicePrice]
                SET
                    [ServiceName] = @ServiceName,
                    [Description] = @Description,
                    [Price] = @Price,
                    [Duration] = @Duration,
                    [IsAvailable] = @IsAvailable
                OUTPUT INSERTED.[Id], INSERTED.[ServiceName] INTO @UpdatedCarWash
                WHERE [Id] = @Id

                SELECT
                    [Id],
                    [ServiceName]
                FROM @UpdatedCarWash;
            ");
        }

        public Task<CarWashServicePriceShortEntity> Delete(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashServicePriceShortEntity>(new
            {
                Id = id
            }, @"
                DECLARE @DeletedCarWashServicePrice TABLE ([Id] INT, [Name] NVARCHAR (50));

                DELETE [company].[CarWashServicePrice]
                OUTPUT DELETED.[Id], DELETED.[Name] INTO @DeletedCarWashServicePrice
                FROM [company].[CarWashServicePrice]
                WHERE [Id] = @Id;

                SELECT
                    [Id],
                    [Name]
                FROM @DeletedCarWashServicePrice;
            ");
        }
    }
}