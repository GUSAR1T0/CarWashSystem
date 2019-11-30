using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class CompanyProfileService : ICompanyProfileService
    {
        private readonly IUserAuthenticationStore userAuthenticationStore;
        private readonly ICompanyProfileStore companyProfileStore;
        private readonly ICarWashStore carWashStore;

        public CompanyProfileService(IUserAuthenticationStore userAuthenticationStore, ICompanyProfileStore companyProfileStore, ICarWashStore carWashStore)
        {
            this.userAuthenticationStore = userAuthenticationStore;
            this.companyProfileStore = companyProfileStore;
            this.carWashStore = carWashStore;
        }

        #region Company Profile

        public async Task<CompanyProfileEntity> GetCompanyFullProfile(IOperation operation, int id)
        {
            return await companyProfileStore.GetCompanyProfileById(operation, id);
        }

        public async Task UpdateCompanyFullProfile(IOperation operation, CompanyProfileEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            await companyProfileStore.UpdateCompanyProfile(operation, entity);
        }

        #endregion

        #region Car Wash

        public async Task<IEnumerable<CarWashShortEntity>> GetCarWashListByCompany(IOperation operation, int companyId)
        {
            return await carWashStore.GetAllShortByCompanyId(operation, companyId);
        }

        public async Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id)
        {
            if (!await carWashStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.GetById(operation, id);
        }

        public async Task<CarWashShortEntity> AddCarWash(IOperation operation, int companyId, CarWashFullEntity entity)
        {
            return await carWashStore.Add(operation, companyId, entity);
        }

        public async Task<CarWashShortEntity> UpdateCarWash(IOperation operation, CarWashFullEntity entity)
        {
            if (!await carWashStore.IsExist(operation, entity.Id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.Update(operation, entity);
        }

        public async Task<CarWashShortEntity> DeleteCarWash(IOperation operation, int id)
        {
            if (await carWashStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.Delete(operation, id);
        }

        #endregion
    }
}