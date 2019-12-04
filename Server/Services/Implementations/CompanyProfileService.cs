using System;
using System.Collections.Generic;
using System.Linq;
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
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email, entity.Id)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            await companyProfileStore.UpdateCompanyProfile(operation, entity);
        }

        #endregion

        #region Car Wash

        public async Task<IEnumerable<CarWashShortEntity>> GetCarWashListShortByCompany(IOperation operation, int companyId)
        {
            return await carWashStore.GetAllShortByCompanyId(operation, companyId);
        }

        public async Task<IEnumerable<CarWashFullEntity>> GetCarWashListFullByCompany(IOperation operation, int userId)
        {
            return await carWashStore.GetAllFullByCompanyId(operation, userId);
        }

        public async Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id)
        {
            if (!await carWashStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.GetById(operation, id);
        }

        public async Task<CarWashShortEntity> AddCarWash(IOperation operation, int userId, CarWashFullEntity entity)
        {
            var invalidatedHours = ValidateCarWashWorkingHours(entity).ToList();
            if (invalidatedHours.Any()) throw new Exception(ExceptionMessage.IncorrectWorkingHoursData(invalidatedHours)); 
            return await carWashStore.Add(operation, userId, entity);
        }

        public async Task<CarWashShortEntity> UpdateCarWash(IOperation operation, CarWashFullEntity entity)
        {
            if (!await carWashStore.IsExist(operation, entity.Id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            var invalidatedHours = ValidateCarWashWorkingHours(entity).ToList();
            if (invalidatedHours.Any()) throw new Exception(ExceptionMessage.IncorrectWorkingHoursData(invalidatedHours)); 
            return await carWashStore.Update(operation, entity);
        }

        public async Task<CarWashShortEntity> DeleteCarWash(IOperation operation, int id)
        {
            if (!await carWashStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.Delete(operation, id);
        }

        private static IEnumerable<string> ValidateCarWashWorkingHours(CarWashFullEntity entity)
        {
            if (!(entity.MondayStartTime.HasValue && entity.MondayStopTime.HasValue || !entity.MondayStartTime.HasValue && !entity.MondayStopTime.HasValue))
            {
                yield return "Monday";
            }

            if (!(entity.TuesdayStartTime.HasValue && entity.TuesdayStopTime.HasValue || !entity.TuesdayStartTime.HasValue && !entity.TuesdayStopTime.HasValue))
            {
                yield return "Tuesday";
            }

            if (!(entity.WednesdayStartTime.HasValue && entity.WednesdayStopTime.HasValue || !entity.WednesdayStartTime.HasValue && !entity.WednesdayStopTime.HasValue))
            {
                yield return "Wednesday";
            }

            if (!(entity.ThursdayStartTime.HasValue && entity.ThursdayStopTime.HasValue || !entity.ThursdayStartTime.HasValue && !entity.ThursdayStopTime.HasValue))
            {
                yield return "Thursday";
            }

            if (!(entity.FridayStartTime.HasValue && entity.FridayStopTime.HasValue || !entity.FridayStartTime.HasValue && !entity.FridayStopTime.HasValue))
            {
                yield return "Friday";
            }

            if (!(entity.SaturdayStartTime.HasValue && entity.SaturdayStopTime.HasValue || !entity.SaturdayStartTime.HasValue && !entity.SaturdayStopTime.HasValue))
            {
                yield return "Saturday";
            }

            if (!(entity.SundayStartTime.HasValue && entity.SundayStopTime.HasValue || !entity.SundayStartTime.HasValue && !entity.SundayStopTime.HasValue))
            {
                yield return "Sunday";
            }
        }

        #endregion
    }
}