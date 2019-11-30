using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Implementations
{
    public class CompanyProfileStore : ICompanyProfileStore
    {
        public async Task<CompanyProfileEntity> GetCompanyProfileById(IOperation operation, int id)
        {
            return await operation.QuerySingleOrDefaultAsync<CompanyProfileEntity>(new
            {
                Id = id
            }, @"
                SELECT
                    aiu.[Email],
                    cc.[Name],
                    cc.[Phone]
                FROM [company].[Company] cc
                INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                WHERE cc.[UserId] = @Id;
            ");
        }

        public async Task UpdateCompanyProfile(IOperation operation, CompanyProfileEntity entity)
        {
            await operation.ExecuteAsync(entity, @"
                UPDATE cc
                SET
                    [Name] = @Name,
                    [Phone] = @Phone
                FROM [company].[Company] cc
                WHERE cc.[UserId] = @Id;

                UPDATE aiu
                SET [Email] = @Email
                FROM [company].[Company] cc
                INNER JOIN [authentication].[User] au ON cc.[UserId] = au.[Id]
                INNER JOIN [authentication].[InternalUser] aiu ON au.[InternalUserId] = aiu.[Id]
                WHERE cc.[UserId] = @Id;
            ");
        }
    }
}