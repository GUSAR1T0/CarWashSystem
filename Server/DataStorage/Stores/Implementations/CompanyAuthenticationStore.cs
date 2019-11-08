using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CompanyAuthenticationStore : ICompanyAuthenticationStore
    {
        public async Task<bool> IsActive(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<bool>(new
            {
                Id = id
            }, @"
                SELECT TOP 1 1
                FROM [authentication].[Company]
                WHERE [Id] = @Id AND [IsActive] = 1;
            ");
        }

        public async Task<CompanyProfileEntity?> GetProfileById(IOperation operation, int id)
        {
            return await operation.QueryFirstOrDefaultAsync<CompanyProfileEntity?>(new
            {
                Id = id
            }, @"
                SELECT
                    [Id],
                    [Name],
                    [Email]
                FROM [authentication].[Company]
                WHERE [Id] = @Id;
            ");
        }

        public async Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                SELECT TOP 1 [Id]
                FROM [authentication].[Company]
                WHERE [Email] = @Email AND [Password] = @Password AND [IsActive] = 1;
            ");
        }

        public async Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            return await operation.QuerySingleOrDefaultAsync<int?>(entity, @"
                DECLARE @Id TABLE ([Id] INT);
                INSERT INTO [authentication].[Company] (
                    [Name],
                    [Email],
                    [Password]
                )
                OUTPUT INSERTED.[Id] INTO @Id
                VALUES (
                    @Name,
                    @Email,
                    @Password
                );
                SELECT [Id] FROM @Id;
            ");
        }
    }
}