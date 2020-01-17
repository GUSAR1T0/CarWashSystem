using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class AppointmentStore : IAppointmentStore
    {
        public async Task<IEnumerable<AppointmentShowItemEntity>> GetListByClient(IOperation operation, int userId)
        {
            return await operation.QueryAsync<AppointmentShowItemEntity>(new
            {
                UserId = userId
            }, @"
                SELECT
                    aa.[Id],
                    ccl.[FirstName],
                    ccl.[LastName],
                    cc.[ModelId]             [CarModelId],
                    cc.[GovernmentPlate]     [CarGovernmentPlate],
                    ccw.[Id]                 [CarWashId],
                    ccw.[Name]               [CarWashName],
                    ccw.[Location]           [CarWashLocation],
                    aa.[RequestedStartTime],
                    aa.[ApprovedStartTime],
                    aa.[StatusId]            [Status]
                FROM [appointment].[Appointment] aa
                INNER JOIN [client].[Car] cc ON cc.[Id] = aa.[CarId]
                INNER JOIN [client].[Client] ccl ON ccl.[Id] = cc.[ClientId]
                INNER JOIN [company].[CarWash] ccw ON ccw.[Id] = aa.[CarWashId]
                WHERE ccl.[UserId] = @UserId;
            ");
        }

        public async Task<IEnumerable<AppointmentShowItemEntity>> GetListByCarWash(IOperation operation, int carWashId)
        {
            return await operation.QueryAsync<AppointmentShowItemEntity>(new
            {
                CarWashId = carWashId
            }, @"
                SELECT
                    aa.[Id],
                    ccl.[FirstName],
                    ccl.[LastName],
                    cc.[ModelId]             [CarModelId],
                    cc.[GovernmentPlate]     [CarGovernmentPlate],
                    ccw.[Id]                 [CarWashId],
                    ccw.[Name]               [CarWashName],
                    ccw.[Location]           [CarWashLocation],
                    aa.[RequestedStartTime],
                    aa.[ApprovedStartTime],
                    aa.[StatusId]            [Status]
                FROM [appointment].[Appointment] aa
                INNER JOIN [client].[Car] cc ON cc.[Id] = aa.[CarId]
                INNER JOIN [client].[Client] ccl ON ccl.[Id] = cc.[ClientId]
                INNER JOIN [company].[CarWash] ccw ON ccw.[Id] = aa.[CarWashId]
                WHERE aa.[CarWashId] = @CarWashId;
            ");
        }

        public async Task<AppointmentShowFullItemEntity> Get(IOperation operation, int appointmentId)
        {
            return await operation.QuerySingleOrDefaultAsync<AppointmentShowFullItemEntity>(new
            {
                Id = appointmentId
            }, @"
                SELECT
                    aa.[Id],
                    ccl.[FirstName],
                    ccl.[LastName],
                    ccl.[Phone],
                    ISNULL(aiu.[Email], aeu.[Email])    [Email],
                    cc.[ModelId]                        [CarModelId],
                    cc.[GovernmentPlate]                [CarGovernmentPlate],
                    ccw.[Id]                            [CarWashId],
                    ccw.[Name]                          [CarWashName],
                    ccw.[Location]                      [CarWashLocation],
                    aa.[RequestedStartTime],
                    aa.[ApprovedStartTime],
                    aa.[StatusId]                       [Status]
                FROM [appointment].[Appointment] aa
                INNER JOIN [client].[Car] cc ON cc.[Id] = aa.[CarId]
                INNER JOIN [client].[Client] ccl ON ccl.[Id] = cc.[ClientId]
                INNER JOIN [authentication].[User] au ON au.[Id] = ccl.[UserId]
                LEFT JOIN [authentication].[InternalUser] aiu ON aiu.[Id] = au.[InternalUserId]
                LEFT JOIN [authentication].[ExternalUser] aeu ON aeu.[Id] = au.[ExternalUserId]
                INNER JOIN [company].[CarWash] ccw ON ccw.[Id] = aa.[CarWashId]
                WHERE aa.[Id] = @Id;
            ");
        }

        public async Task<bool> IsExist(IOperation operation, int appointmentId)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = appointmentId
            }, @"
                SELECT TOP 1 1
                FROM [appointment].[Appointment]
                WHERE [Id] = @Id;
            ");
        }

        public async Task<int> Add(IOperation operation, AppointmentManageItemEntity entity)
        {
            return await operation.QuerySingleAsync<int>(new
            {
                entity.CarId,
                entity.CarWashId,
                entity.StartTime,
                entity.CarWashServiceIds
            }, @"
                DECLARE @IdTable TABLE ([Id] INT);

                INSERT INTO [appointment].[Appointment] (
                    [CarId],
                    [CarWashId],
                    [RequestedStartTime],
                    [StatusId]
                )
                OUTPUT INSERTED.[Id] INTO @IdTable
                VALUES (
                    @CarId,
                    @CarWashId,
                    @StartTime,
                    1 -- Opened
                );

                DECLARE @AppointmentId INT;
                SELECT @AppointmentId = [Id] FROM @IdTable;

                INSERT INTO [appointment].[AppointmentCarWashService] (
                    [AppointmentId],
                    [CarWashServiceId]
                )
                SELECT
                    @AppointmentId,
                    [Id]
                FROM [company].[CarWashService]
                WHERE [Id] IN @CarWashServiceIds;

                SELECT @AppointmentId;
            ");
        }

        public async Task UpdateAsClient(IOperation operation, AppointmentManageItemEntity entity)
        {
            await operation.ExecuteAsync(new
            {
                entity.Id,
                entity.StartTime,
                entity.CarWashServiceIds
            }, @"
                UPDATE
                SET [RequestedStartTime] = @StartTime
                FROM [appointment].[Appointment]
                WHERE [Id] = @Id;

                MERGE INTO [appointment].[AppointmentCarWashService] t
                USING (
                    SELECT
                        @Id    [AppointmentId],
                        [Id]   [CarWashServiceId]
                    FROM [company].[CarWashService]
                    WHERE [CarWashServiceId] IN @CarWashServiceIds
                ) s ON t.[AppointmentId] = s.[AppointmentId] AND t.[CarWashServiceId] = s.[CarWashServiceId]
                WHEN NOT MATCHED BY TARGET THEN INSERT (
                    [AppointmentId],
                    [CarWashServiceId]
                )
                VALUES (
                    s.[AppointmentId],
                    s.[CarWashServiceId]
                )
                WHEN NOT MATCHED BY SOURCE THEN DELETE;
            ");
        }

        public async Task UpdateAsCompany(IOperation operation, AppointmentManageItemEntity entity)
        {
            await operation.ExecuteAsync(new
            {
                entity.Id,
                entity.StartTime
            }, @"
                UPDATE [appointment].[Appointment]
                SET [ApprovedStartTime] = @StartTime
                WHERE [Id] = @Id;
            ");
        }

        public async Task ChangeStatus(IOperation operation, int appointmentId, AppointmentStatus status)
        {
            await operation.ExecuteAsync(new
            {
                Id = appointmentId,
                Status = status
            }, @"
                UPDATE [appointment].[Appointment]
                SET [StatusId] = @Status
                WHERE [Id] = @Id;
            ");
        }

        public async Task<IEnumerable<CarWashServiceEntity>> GetServicesByAppointment(IOperation operation, int appointmentId)
        {
            return await operation.QueryAsync<CarWashServiceEntity>(new
            {
                AppointmentId = appointmentId
            }, @"
                SELECT
                    ccws.[Id],
                    ccws.[CarWashId],
                    ccws.[ServiceName],
                    ccws.[Description],
                    ccws.[Price],
                    ccws.[Duration],
                    ccws.[IsAvailable]
                FROM [appointment].[AppointmentCarWashService] aacws
                INNER JOIN [company].[CarWashService] ccws ON ccws.[Id] = aacws.[CarWashServiceId]
                WHERE aacws.[AppointmentId] = @AppointmentId;
            ");
        }

        public async Task<IEnumerable<AppointmentHistoryEntity>> GetHistoryRecords(IOperation operation, int appointmentId)
        {
            return await operation.QueryAsync<AppointmentHistoryEntity>(new
            {
                AppointmentId = appointmentId
            }, @"
                SELECT
                    [Id],
                    [AppointmentId],
                    [Action],
                    [Timestamp]
                FROM [appointment].[AppointmentHistory]
                WHERE [AppointmentId] = @AppointmentId;
            ");
        }

        public async Task AddHistoryRecord(IOperation operation, int appointmentId, string action)
        {
            await operation.ExecuteAsync(new
            {
                AppointmentId = appointmentId,
                Action = action,
            }, @"
                INSERT INTO [appointment].[AppointmentHistory] (
                    [AppointmentId],
                    [Action]
                )
                VALUES (
                    @AppointmentId,
                    @Action
                );
            ");
        }
    }
}