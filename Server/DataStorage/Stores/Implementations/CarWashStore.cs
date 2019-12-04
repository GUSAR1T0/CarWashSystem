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

        public Task<IEnumerable<CarWashFullEntity>> GetAllFullByCompanyId(IOperation operation, int userId)
        {
            return operation.QueryAsync<CarWashFullEntity>(new
            {
                UserId = userId
            }, @"
                SELECT
                    ccw.[Id],
                    ccw.[Name],
                    ccw.[Email],
                    ccw.[Phone],
                    ccw.[Location],
                    ccw.[CoordinateX],
                    ccw.[CoordinateY],
                    ccw.[Description],
                    ccw.[HasCafe],
                    ccw.[HasRestZone],
                    ccw.[HasParking],
                    ccw.[HasWC],
                    ccw.[HasCardPayment],
                    ccwwh.[MondayStartTime],
                    ccwwh.[MondayStopTime],
                    ccwwh.[TuesdayStartTime],
                    ccwwh.[TuesdayStopTime],
                    ccwwh.[WednesdayStartTime],
                    ccwwh.[WednesdayStopTime],
                    ccwwh.[ThursdayStartTime],
                    ccwwh.[ThursdayStopTime],
                    ccwwh.[FridayStartTime],
                    ccwwh.[FridayStopTime],
                    ccwwh.[SaturdayStartTime],
                    ccwwh.[SaturdayStopTime],
                    ccwwh.[SundayStartTime],
                    ccwwh.[SundayStopTime]
                FROM [company].[CarWash] ccw
                INNER JOIN [company].[Company] cc ON ccw.[CompanyId] = cc.[Id]
                INNER JOIN [company].[CarWashWorkingHours] ccwwh ON ccw.[Id] = ccwwh.[CarWashId]
                WHERE cc.[UserId] = @UserId
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
                    ccw.[Id],
                    ccw.[Name],
                    ccw.[Email],
                    ccw.[Phone],
                    ccw.[Location],
                    ccw.[CoordinateX],
                    ccw.[CoordinateY],
                    ccw.[Description],
                    ccw.[HasCafe],
                    ccw.[HasRestZone],
                    ccw.[HasParking],
                    ccw.[HasWC],
                    ccw.[HasCardPayment],
                    ccwwh.[MondayStartTime],
                    ccwwh.[MondayStopTime],
                    ccwwh.[TuesdayStartTime],
                    ccwwh.[TuesdayStopTime],
                    ccwwh.[WednesdayStartTime],
                    ccwwh.[WednesdayStopTime],
                    ccwwh.[ThursdayStartTime],
                    ccwwh.[ThursdayStopTime],
                    ccwwh.[FridayStartTime],
                    ccwwh.[FridayStopTime],
                    ccwwh.[SaturdayStartTime],
                    ccwwh.[SaturdayStopTime],
                    ccwwh.[SundayStartTime],
                    ccwwh.[SundayStopTime]
                FROM [company].[CarWash] ccw
                INNER JOIN [company].[CarWashWorkingHours] ccwwh ON ccw.[Id] = ccwwh.[CarWashId]
                WHERE ccw.[Id] = @Id
            ");
        }

        public Task<CarWashShortEntity> Add(IOperation operation, int userId, CarWashFullEntity entity)
        {
            return operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(new
            {
                UserId = userId,
                entity.Name,
                entity.Email,
                entity.Phone,
                entity.Location,
                entity.CoordinateX,
                entity.CoordinateY,
                entity.Description,
                entity.HasCafe,
                entity.HasRestZone,
                entity.HasParking,
                entity.HasWC,
                entity.HasCardPayment,
                entity.MondayStartTime,
                entity.MondayStopTime,
                entity.TuesdayStartTime,
                entity.TuesdayStopTime,
                entity.WednesdayStartTime,
                entity.WednesdayStopTime,
                entity.ThursdayStartTime,
                entity.ThursdayStopTime,
                entity.FridayStartTime,
                entity.FridayStopTime,
                entity.SaturdayStartTime,
                entity.SaturdayStopTime,
                entity.SundayStartTime,
                entity.SundayStopTime
            }, @"
                DECLARE @NewCarWash TABLE ([Id] INT, [Name] NVARCHAR (50));

                INSERT INTO [company].[CarWash] (
                    [CompanyId],
                    [Name],
                    [Email],
                    [Phone],
                    [Location],
                    [CoordinateX],
                    [CoordinateY],
                    [Description],
                    [HasCafe],
                    [HasRestZone],
                    [HasParking],
                    [HasWC],
                    [HasCardPayment]
                )
                OUTPUT INSERTED.[Id], INSERTED.[Name] INTO @NewCarWash
                SELECT
                    [Id],
                    @Name,
                    @Email,
                    @Phone,
                    @Location,
                    @CoordinateX,
                    @CoordinateY,
                    @Description,
                    @HasCafe,
                    @HasRestZone,
                    @HasParking,
                    @HasWC,
                    @HasCardPayment
                FROM [company].[Company]
                WHERE [UserId] = @UserId;

                INSERT INTO [company].[CarWashWorkingHours] (
                    [CarWashId],
                    [MondayStartTime],
                    [MondayStopTime],
                    [TuesdayStartTime],
                    [TuesdayStopTime],
                    [WednesdayStartTime],
                    [WednesdayStopTime],
                    [ThursdayStartTime],
                    [ThursdayStopTime],
                    [FridayStartTime],
                    [FridayStopTime],
                    [SaturdayStartTime],
                    [SaturdayStopTime],
                    [SundayStartTime],
                    [SundayStopTime]
                )
                SELECT
                    ncw.[Id],
                    @MondayStartTime,
                    @MondayStopTime,
                    @TuesdayStartTime,
                    @TuesdayStopTime,
                    @WednesdayStartTime,
                    @WednesdayStopTime,
                    @ThursdayStartTime,
                    @ThursdayStopTime,
                    @FridayStartTime,
                    @FridayStopTime,
                    @SaturdayStartTime,
                    @SaturdayStopTime,
                    @SundayStartTime,
                    @SundayStopTime
                FROM @NewCarWash ncw;

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
                    [CoordinateX] = @CoordinateX,
                    [CoordinateY] = @CoordinateY,
                    [Description] = @Description,
                    [HasCafe] = @HasCafe,
                    [HasRestZone] = @HasRestZone,
                    [HasParking] = @HasParking,
                    [HasWC] = @HasWC,
                    [HasCardPayment] = @HasCardPayment
                OUTPUT INSERTED.[Id], INSERTED.[Name] INTO @UpdatedCarWash
                WHERE [Id] = @Id;

                UPDATE [company].[CarWashWorkingHours]
                SET
                    [MondayStartTime] = @MondayStartTime,
                    [MondayStopTime] = @MondayStopTime,
                    [TuesdayStartTime] = @TuesdayStartTime,
                    [TuesdayStopTime] = @TuesdayStopTime,
                    [WednesdayStartTime] = @WednesdayStartTime,
                    [WednesdayStopTime] = @WednesdayStopTime,
                    [ThursdayStartTime] = @ThursdayStartTime,
                    [ThursdayStopTime] = @ThursdayStopTime,
                    [FridayStartTime] = @FridayStartTime,
                    [FridayStopTime] = @FridayStopTime,
                    [SaturdayStartTime] = @SaturdayStartTime,
                    [SaturdayStopTime] = @SaturdayStopTime,
                    [SundayStartTime] = @SundayStartTime,
                    [SundayStopTime] = @SundayStopTime
                WHERE [CarWashId] = @Id;

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

                -- Working Hours data should be automatically removed after cascade action

                SELECT
                    [Id],
                    [Name]
                FROM @DeletedCarWash;
            ");
        }
    }
}