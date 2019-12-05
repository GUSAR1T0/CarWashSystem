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

        #region Car Washes

        Task<IEnumerable<CarWashShortEntity>> GetCarWashListByCompany(IOperation operation, int userId);
        Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id);
        Task<CarWashShortEntity> AddCarWash(IOperation operation, int userId, CarWashFullEntity entity);
        Task<CarWashShortEntity> UpdateCarWash(IOperation operation, CarWashFullEntity entity);
        Task<CarWashShortEntity> DeleteCarWash(IOperation operation, int id);

        #endregion

        #region Car Wash Services

        Task<IEnumerable<CarWashServiceEntity>> GetCarWashServiceListByCarWash(IOperation operation, int carWashId);
        Task<CarWashServiceShortEntity> AddCarWashService(IOperation operation, int carWashId, CarWashServiceEntity entity);
        Task<CarWashServiceShortEntity> UpdateCarWashService(IOperation operation, CarWashServiceEntity entity);
        Task<CarWashServiceShortEntity> DeleteCarWashService(IOperation operation, int id);

        #endregion
    }
}