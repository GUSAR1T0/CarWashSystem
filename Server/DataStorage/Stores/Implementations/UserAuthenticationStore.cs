using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class UserAuthenticationStore : IUserAuthenticationStore
    {
        #region User

        public async Task<bool> IsActive(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [authentication].[User]
                WHERE [Id] = @Id AND [IsActive] = 1;
            ");
        }

        public async Task<bool> IsUserExist(IOperation operation, string email)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Email = email
            }, @"
                SELECT TOP 1 1
                FROM [authentication].[User]
                WHERE [Email] = @Email;
            ");
        }

        #endregion

        #region Company

        public async Task<CompanyProfileEntity?> GetCompanyProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CompanyProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    au.[Id],
                    au.[Email],
                    ac.[Name]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[Company] ac ON au.[CompanyId] = ac.[Id]
                WHERE au.[Id] = @Id;
            ");
        }

        public async Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                SELECT TOP 1 [Id]
                FROM [authentication].[User]
                WHERE [Email] = @Email AND [Password] = @Password AND [CompanyId] IS NOT NULL AND [IsActive] = 1;
            ");
        }

        public async Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                DECLARE @CompanyId TABLE ([Id] INT);
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO [authentication].[Company] ([Name])
                OUTPUT INSERTED.[Id] INTO @CompanyId
                VALUES (@Name);

                INSERT INTO [authentication].[User] (
                    [Email],
                    [Password],
                    [CompanyId]
                )
                OUTPUT INSERTED.[Id] INTO @UserId
                SELECT 
                    @Email,
                    @Password,
                    [Id]
                FROM @CompanyId;

                SELECT [Id] FROM @UserId;
            ");
        }

        #endregion

        #region Client

        public async Task<ClientProfileEntity?> GetClientProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<ClientProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    au.[Id],
                    ac.[FirstName],
                    ac.[LastName]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[Client] ac ON au.[ClientId] = ac.[Id]
                WHERE au.[Id] = @Id;
            ");
        }

        public async Task<int?> TrySignIn(IOperation operation, ClientSignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                SELECT TOP 1 au.[Id]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[Client] ac ON ac.[Id] = au.[ClientId]
                WHERE au.[Email] = @Email AND au.[Password] = @Password AND ac.[ExternalClientId] IS NULL AND au.[IsActive] = 1;
            ");
        }

        public async Task<int?> TrySignUp(IOperation operation, ClientSignUpEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                DECLARE @ClientId TABLE ([Id] INT);
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO [authentication].[Client] (
                    [FirstName],
                    [LastName]
                )
                OUTPUT INSERTED.[Id] INTO @ClientId
                VALUES (
                    @FirstName,
                    @LastName
                );

                INSERT INTO [authentication].[User] (
                    [Email],
                    [Password],
                    [ClientId]
                )
                OUTPUT INSERTED.[Id] INTO @UserId
                SELECT
                    @Email,
                    @Password,
                    [Id]
                FROM @ClientId;

                SELECT [Id] FROM @UserId;
            ");
        }

        public async Task<Guid?> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<Guid?>(entity, @"
                DECLARE @ClientId TABLE ([Id] INT);
                DECLARE @UserId TABLE ([Id] INT, [IsActive] BIT);

                INSERT INTO @UserId (
                    [Id],
                    [IsActive]
                )
                SELECT
                    au.[Id],
                    au.[IsActive]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[Client] ac ON ac.[Id] = au.[ClientId]
                INNER JOIN [authentication].[ExternalClient] aec ON aec.[Id] = ac.[ExternalClientId]
                WHERE aec.[Schema] = @Schema AND aec.[ExternalId] = @ExternalId;

                IF NOT EXISTS (SELECT TOP 1 1 FROM @UserId)
                BEGIN

                    DECLARE @ExternalClientId TABLE ([Id] INT);

                    INSERT INTO [authentication].[ExternalClient] (
                        [ExternalId],
                        [Schema]
                    )
                    OUTPUT INSERTED.[Id] INTO @ExternalClientId
                    VALUES (
                        @ExternalId,
                        @Schema
                    );

                    INSERT INTO [authentication].[Client] (
                        [FirstName],
                        [LastName],
                        [ExternalClientId]
                    )
                    OUTPUT INSERTED.[Id] INTO @ClientId
                    SELECT
                        @FirstName,
                        @LastName,
                        i.[Id]
                    FROM @ExternalClientId i;

                    INSERT INTO [authentication].[User] ([ClientId])
                    OUTPUT INSERTED.[Id], 1 INTO @UserId
                    SELECT [Id]
                    FROM @ClientId;

                END;

                DECLARE @ExternalClientAuthenticationToken TABLE ([AuthenticationToken] UNIQUEIDENTIFIER);

                UPDATE aec
                SET aec.[AuthenticationToken] = NEWID()
                OUTPUT INSERTED.[AuthenticationToken] INTO @ExternalClientAuthenticationToken
                FROM [authentication].[ExternalClient] aec
                INNER JOIN [authentication].[Client] ac ON ac.[ExternalClientId] = aec.[Id]
                INNER JOIN [authentication].[User] au ON ac.[Id] = au.[ClientId]
                INNER JOIN @UserId ui ON ui.[Id] = au.[Id]
                WHERE ui.[IsActive] = 1;

                SELECT [AuthenticationToken] FROM @ExternalClientAuthenticationToken;
            ");
        }

        public async Task<int?> TryExternalSignIn(IOperation operation, Guid token)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(new { Token = token }, @"
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO @UserId ([Id])
                SELECT au.[Id]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[Client] ac ON ac.[Id] = au.[ClientId]
                INNER JOIN [authentication].[ExternalClient] aec ON aec.[Id] = ac.[ExternalClientId]
                WHERE aec.[AuthenticationToken] = @Token AND au.[IsActive] = 1;

                UPDATE aec
                SET aec.[AuthenticationToken] = NULL
                FROM [authentication].[ExternalClient] aec
                INNER JOIN [authentication].[Client] ac ON ac.[ExternalClientId] = aec.[Id]
                INNER JOIN [authentication].[User] au ON ac.[Id] = au.[ClientId]
                INNER JOIN @UserId ui ON ui.[Id] = au.[Id];

                SELECT [Id] FROM @UserId;
            ");
        }

        #endregion
    }
}