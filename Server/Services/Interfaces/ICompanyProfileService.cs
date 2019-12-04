using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface ICompanyProfileService
    {
        #region Company Profile

        Task<CompanyProfileEntity> GetCompanyFullProfile(IOperation operation, int id);
        Task UpdateCompanyFullProfile(IOperation operation, CompanyProfileEntity entity);

        #endregion

        #region Car Wash

        Task<IEnumerable<CarWashShortEntity>> GetCarWashListShortByCompany(IOperation operation, int companyId);
        Task<IEnumerable<CarWashFullEntity>> GetCarWashListFullByCompany(IOperation operation, int userId);
        Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id);
        Task<CarWashShortEntity> AddCarWash(IOperation operation, int userId, CarWashFullEntity entity);
        Task<CarWashShortEntity> UpdateCarWash(IOperation operation, CarWashFullEntity entity);
        Task<CarWashShortEntity> DeleteCarWash(IOperation operation, int id);

        #endregion
    }
}