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

        Task<IEnumerable<CarWashFullEntity>> GetCarWashListFullByCompany(IOperation operation, int userId);
        Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id);
        Task<CarWashShortEntity> AddCarWash(IOperation operation, int userId, CarWashFullEntity entity);
        Task<CarWashShortEntity> UpdateCarWash(IOperation operation, CarWashFullEntity entity);
        Task<CarWashShortEntity> DeleteCarWash(IOperation operation, int id);

        #endregion

        #region Car Wash Service Prices

        Task<IEnumerable<CarWashServicePriceEntity>> GetServicePriceListByCompany(IOperation operation, int userId);
        Task<IEnumerable<CarWashServicePriceEntity>> GetServicePriceListByCarWash(IOperation operation, int carWashId);
        Task<CarWashServicePriceShortEntity> AddServicePrice(IOperation operation, int carWashId, CarWashServicePriceEntity entity);
        Task<CarWashServicePriceShortEntity> UpdateServicePrice(IOperation operation, CarWashServicePriceEntity entity);
        Task<CarWashServicePriceShortEntity> DeleteServicePrice(IOperation operation, int id);

        #endregion
    }
}