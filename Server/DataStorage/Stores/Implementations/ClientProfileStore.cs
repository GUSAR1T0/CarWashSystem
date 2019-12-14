using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class ClientProfileStore : IClientProfileStore
    {
        public async Task<ClientProfileEntity> GetClientProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<ClientProfileEntity>(new
            {
                Id = id
            }, @"
                SELECT
                    ISNULL(aiu.[Email], aeu.[Email]) AS [Email],
                    cc.[FirstName],
                    cc.[LastName],
                    cc.[Phone],
                    cc.[Birthday]
                FROM [client].[Client] cc
                INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                LEFT JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                LEFT JOIN [authentication].[ExternalUser] aeu ON au.[ExternalUserId] = aeu.[Id]
                WHERE cc.[UserId] = @Id;
            ");
        }

        public async Task UpdateClientProfile(IOperation operation, ClientProfileEntity entity)
        {
            await operation.ExecuteAsync(entity, @"
                UPDATE cc
                SET
                    [FirstName] = @FirstName,
                    [LastName] = @LastName,
                    [Phone] = @Phone,
                    [Birthday] = @Birthday
                FROM [client].[Client] cc
                WHERE cc.[UserId] = @Id;

                IF EXISTS (
                    SELECT IIF(aiu.[Id] IS NULL, 0, 1)
                    FROM [client].[Client] cc
                    INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                    LEFT JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                    WHERE cc.[UserId] = @Id
                )
                BEGIN

                    UPDATE aiu
                    SET [Email] = @Email
                    FROM [company].[Company] cc
                    INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                    INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                    WHERE cc.[UserId] = @Id;

                END
                ELSE
                BEGIN

                    UPDATE aeu
                    SET [Email] = @Email
                    FROM [company].[Company] cc
                    INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                    INNER JOIN [authentication].[ExternalUser] aeu ON au.[ExternalUserId] = aeu.[Id]
                    WHERE cc.[UserId] = @Id;

                END
            ");
        }
    }
}