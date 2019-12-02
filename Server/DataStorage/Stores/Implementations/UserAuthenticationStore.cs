using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class UserAuthenticationStore : IUserAuthenticationStore
    {
        #region User

        public async Task<bool> IsUserActivated(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [authentication].[User]
                WHERE [Id] = @Id AND [IsActivated] = 1;
            ");
        }

        public async Task<bool> IsUserExist(IOperation operation, string email, int? id = null)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Email = email,
                Id = id
            }, $@"
                SELECT TOP 1 1
                FROM [authentication].[User] au
                INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                WHERE aiu.[Email] = @Email{(id.HasValue ? " AND au.[Id] <> @Id" : "")};
            ");
        }

        #endregion

        #region Company

        public async Task<CompanyAuthenticationProfileEntity?> GetCompanyProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CompanyAuthenticationProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    [UserId] AS [Id],
                    [Name]
                FROM [company].[Company]
                WHERE [UserId] = @Id;
            ");
        }

        public async Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                SELECT TOP 1 au.[Id]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                WHERE
                    EXISTS (
                        SELECT TOP 1 1
                        FROM [company].[Company] cc
                        WHERE cc.[UserId] = au.[Id]
                    ) AND
                    aiu.[Email] = @Email AND
                    aiu.[Password] = @Password AND
                    au.[IsActivated] = 1;
            ");
        }

        public async Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                DECLARE @InternalUserId TABLE ([Id] INT);
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO [authentication].[InternalUser] (
                    [Email],
                    [Password]
                )
                OUTPUT INSERTED.[Id] INTO @InternalUserId
                VALUES (
                    @Email,
                    @Password
                );

                INSERT INTO [authentication].[User] ([InternalUserId])
                OUTPUT INSERTED.[Id] INTO @UserId
                SELECT [Id]
                FROM @InternalUserId;

                INSERT INTO [company].[Company] (
                    [UserId],
                    [Name]
                )
                SELECT
                    [Id],
                    @Name
                FROM @UserId;

                SELECT [Id] FROM @UserId;
            ");
        }

        #endregion

        #region Client

        public async Task<ClientAuthenticationProfileEntity?> GetClientProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<ClientAuthenticationProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    [UserId] AS [Id],
                    [FirstName],
                    [LastName]
                FROM [client].[Client]
                WHERE [UserId] = @Id;
            ");
        }

        public async Task<int?> TrySignIn(IOperation operation, ClientSignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                SELECT TOP 1 au.[Id]
                FROM [authentication].[User] au
                INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                WHERE
                    EXISTS (
                        SELECT TOP 1 1
                        FROM [client].[Client] cc
                        WHERE cc.[UserId] = au.[Id]
                    ) AND
                    aiu.[Email] = @Email AND
                    aiu.[Password] = @Password AND
                    au.[IsActivated] = 1;
            ");
        }

        public async Task<int?> TrySignUp(IOperation operation, ClientSignUpEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                DECLARE @InternalUserId TABLE ([Id] INT);
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO [authentication].[InternalUser] (
                    [Email],
                    [Password]
                )
                OUTPUT INSERTED.[Id] INTO @InternalUserId
                VALUES (
                    @Email,
                    @Password
                );

                INSERT INTO [authentication].[User] ([InternalUserId])
                OUTPUT INSERTED.[Id] INTO @UserId
                SELECT [Id]
                FROM @InternalUserId;

                INSERT INTO [client].[Client] (
                    [UserId],
                    [FirstName],
                    [LastName]
                )
                SELECT
                    [Id],
                    @FirstName,
                    @LastName
                FROM @UserId;

                SELECT [Id] FROM @UserId;
            ");
        }

        public async Task<Guid?> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<Guid?>(entity, @"
                DECLARE @UserId TABLE ([Id] INT, [IsActivated] BIT);

                INSERT INTO @UserId (
                    [Id],
                    [IsActivated]
                )
                SELECT
                    au.[Id],
                    au.[IsActivated]
                FROM [client].[Client] cc
                INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                INNER JOIN [authentication].[ExternalUser] aeu ON au.[ExternalUserId] = aeu.[Id]
                WHERE aeu.[SchemaId] = @Schema AND aeu.[ExternalId] = @ExternalId;

                IF NOT EXISTS (SELECT TOP 1 1 FROM @UserId)
                BEGIN

                    DECLARE @ExternalUserId TABLE ([Id] INT);

                    INSERT INTO [authentication].[ExternalUser] (
                        [ExternalId],
                        [SchemaId]
                    )
                    OUTPUT INSERTED.[Id] INTO @ExternalUserId
                    VALUES (
                        @ExternalId,
                        @Schema
                    );

                    INSERT INTO [authentication].[User] ([ExternalUserId])
                    OUTPUT INSERTED.[Id], 1 INTO @UserId
                    SELECT [Id]
                    FROM @ExternalUserId;

                    INSERT INTO [client].[Client] (
                        [UserId],
                        [FirstName],
                        [LastName]
                    )
                    SELECT
                        [Id],
                        @FirstName,
                        @LastName
                    FROM @UserId;

                END;

                DECLARE @ExternalClientAuthenticationToken TABLE ([AuthenticationToken] UNIQUEIDENTIFIER);

                UPDATE aeu
                SET aeu.[AuthenticationToken] = NEWID()
                OUTPUT INSERTED.[AuthenticationToken] INTO @ExternalClientAuthenticationToken
                FROM [authentication].[User] au
                INNER JOIN @UserId ui ON au.[Id] = ui.[Id]
                INNER JOIN [authentication].[ExternalUser] aeu ON au.[ExternalUserId] = aeu.[Id]
                WHERE ui.[IsActivated] = 1;

                SELECT [AuthenticationToken] FROM @ExternalClientAuthenticationToken;
            ");
        }

        public async Task<int?> TryExternalSignIn(IOperation operation, Guid token)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(new { Token = token }, @"
                DECLARE @UserId TABLE ([Id] INT);

                INSERT INTO @UserId ([Id])
                SELECT au.[Id]
                FROM [client].[Client] cc
                INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                INNER JOIN [authentication].[ExternalUser] aeu ON au.[ExternalUserId] = aeu.[Id]
                WHERE aeu.[AuthenticationToken] = @Token AND au.[IsActivated] = 1;

                UPDATE aeu
                SET aeu.[AuthenticationToken] = NULL
                FROM [authentication].[ExternalUser] aeu
                INNER JOIN [authentication].[User] au ON aeu.[Id] = au.[ExternalUserId]
                INNER JOIN @UserId ui ON au.[Id] = ui.[Id];

                SELECT [Id] FROM @UserId;
            ");
        }

        #endregion
    }
}