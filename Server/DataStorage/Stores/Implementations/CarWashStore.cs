using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CarWashStore : ICarWashStore
    {
        public async Task<IEnumerable<CarWashFullEntity>> GetAll(IOperation operation)
        {
            return await operation.QueryAsync<CarWashFullEntity>(@"
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
                INNER JOIN [company].[CarWashWorkingHours] ccwwh ON ccw.[Id] = ccwwh.[CarWashId];
            ");
        }

        public async Task<IEnumerable<CarWashShortEntity>> GetAllByUserId(IOperation operation, int userId)
        {
            return await operation.QueryAsync<CarWashShortEntity>(new
            {
                UserId = userId
            }, @"
                SELECT
                    ccw.[Id],
                    ccw.[Name],
                    ccw.[Location]
                FROM [company].[CarWash] ccw
                INNER JOIN [company].[Company] cc ON ccw.[CompanyId] = cc.[Id]
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
                FROM [company].[CarWash]
                WHERE [Id] = @Id;
            ");
        }

        public async Task<CarWashFullEntity> GetById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CarWashFullEntity>(new
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
                WHERE ccw.[Id] = @Id;
            ");
        }

        public async Task<CarWashShortEntity> Add(IOperation operation, int userId, CarWashFullEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(new
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
                DECLARE @NewCarWash TABLE ([Id] INT, [Name] NVARCHAR (50), [Location] NVARCHAR (512));

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
                OUTPUT INSERTED.[Id], INSERTED.[Name], INSERTED.[Location] INTO @NewCarWash
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
                    [Name],
                    [Location]
                FROM @NewCarWash;
            ");
        }

        public async Task<CarWashShortEntity> Update(IOperation operation, CarWashFullEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(entity, @"
                DECLARE @UpdatedCarWash TABLE ([Id] INT, [Name] NVARCHAR (50), [Location] NVARCHAR (512));

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
                OUTPUT INSERTED.[Id], INSERTED.[Name] INSERTED.[Location] INTO @UpdatedCarWash
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
                    [Name],
                    [Location]
                FROM @UpdatedCarWash;
            ");
        }

        public async Task<CarWashShortEntity> Delete(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CarWashShortEntity>(new
            {
                Id = id
            }, @"
                DECLARE @DeletedCarWash TABLE ([Id] INT, [Name] NVARCHAR (50), [Location] NVARCHAR (512));

                DELETE [company].[CarWash]
                OUTPUT DELETED.[Id], DELETED.[Name], DELETED.[Location] INTO @DeletedCarWash
                FROM [company].[CarWash]
                WHERE [Id] = @Id;

                -- Working Hours and Service Prices data should be automatically removed after cascade action

                SELECT
                    [Id],
                    [Name],
                    [Location]
                FROM @DeletedCarWash;
            ");
        }
    }
}