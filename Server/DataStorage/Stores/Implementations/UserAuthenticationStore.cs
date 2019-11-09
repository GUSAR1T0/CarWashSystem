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

        public async Task<UserProfileEntity?> GetUserProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<UserProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    [Id],
                    [Email]
                FROM [authentication].[User]
                WHERE [Id] = @Id;
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
                    au.[Email],
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
                SELECT TOP 1 [Id]
                FROM [authentication].[User]
                WHERE [Email] = @Email AND [Password] = @Password AND [ClientId] IS NOT NULL AND [IsActive] = 1;
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

        #endregion
    }
}