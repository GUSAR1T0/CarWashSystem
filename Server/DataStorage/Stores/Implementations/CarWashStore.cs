using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarWashStore : ICarWashStore
    {
        public Task<IEnumerable<CarWashShortEntity>> GetAllShortByCompanyId(IOperation operation, int companyId)
        {
            return operation.QueryAsync<CarWashShortEntity>(new
            {
                CompanyId = companyId
            }, @"
                SELECT
                    [Id],
                    [Name]
                FROM [company].[CarWash]
                WHERE [CompanyId] = @CompanyId
            ");
        }

        public Task<bool> IsExist(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [company].[CarWash]
                WHERE [Id] = @Id
            ");
        }

        public Task<CarWashFullEntity> GetById(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashFullEntity>(new
            {
                Id = id
            }, @"
                SELECT
                    [Id],
                    [Name],
                    [Email],
                    [Phone],
                    [Location],
                    [HasCafe],
                    [HasRestZone],
                    [HasParking],
                    [HasWC],
                    [HasCardPayment]
                FROM [company].[CarWash]
                WHERE [Id] = @Id
            ");
        }

        public Task<CarWashShortEntity> Add(IOperation operation, int companyId, CarWashFullEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(new
            {
                CompanyId = companyId,
                entity.Name,
                entity.Email,
                entity.Phone,
                entity.Location,
                entity.HasCafe,
                entity.HasRestZone,
                entity.HasParking,
                entity.HasWC,
                entity.HasCardPayment
            }, @"
                DECLARE @NewCarWash TABLE ([Id] INT, [Name] NVARCHAR (50));

                INSERT INTO [company].[CarWash] (
                    [CompanyId],
                    [Name],
                    [Email],
                    [Phone],
                    [Location],
                    [HasCafe],
                    [HasRestZone],
                    [HasParking],
                    [HasWC],
                    [HasCardPayment]
                )
                OUTPUT INSERTED.[Id], INSERTED.[Name] INTO @NewCarWash
                VALUES (
                    @CompanyId,
                    @Name,
                    @Email,
                    @Phone,
                    @Location,
                    @HasCafe,
                    @HasRestZone,
                    @HasParking,
                    @HasWC,
                    @HasCardPayment
                );

                SELECT
                    [Id],
                    [Name]
                FROM @NewCarWash;
            ");
        }

        public Task<CarWashShortEntity> Update(IOperation operation, CarWashFullEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(entity, @"
                DECLARE @UpdatedCarWash TABLE ([Id] INT, [Name] NVARCHAR (50));

                UPDATE [company].[CarWash]
                SET
                    [Name] = @Name,
                    [Email] = @Email,
                    [Phone] = @Phone,
                    [Location] = @Location,
                    [HasCafe] = @HasCafe,
                    [HasRestZone] = @HasRestZone,
                    [HasParking] = @HasParking,
                    [HasWC] = @HasWC,
                    [HasCardPayment] = @HasCardPayment
                OUTPUT INSERTED.[Id], INSERTED.[Name] INTO @UpdatedCarWash
                WHERE [Id] = @Id;

                SELECT
                    [Id],
                    [Name]
                FROM @UpdatedCarWash;
            ");
        }

        public Task<CarWashShortEntity> Delete(IOperation operation, int id)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(new
            {
                Id = id
            }, @"
                DECLARE @DeletedCarWash TABLE ([Id] INT, [Name] NVARCHAR (50));

                DELETE [company].[CarWash]
                OUTPUT DELETED.[Id], DELETED.[Name] INTO @DeletedCarWash
                FROM [company].[CarWash]
                WHERE [Id] = @Id;

                SELECT
                    [Id],
                    [Name]
                FROM @DeletedCarWash;
            ");
        }
    }
}