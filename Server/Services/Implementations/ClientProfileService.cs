using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class ClientProfileService : IClientProfileService
    {
        private readonly IUserAuthenticationStore userAuthenticationStore;
        private readonly IClientProfileStore clientProfileStore;
        private readonly ICarStore carStore;

        public ClientProfileService(IUserAuthenticationStore userAuthenticationStore, IClientProfileStore clientProfileStore, ICarStore carStore)
        {
            this.userAuthenticationStore = userAuthenticationStore;
            this.clientProfileStore = clientProfileStore;
            this.carStore = carStore;
        }

        #region Client Profile

        public async Task<ClientProfileEntity> GetClientFullProfile(IOperation operation, int id)
        {
            return await clientProfileStore.GetClientProfileById(operation, id);
        }

        public async Task UpdateClientFullProfile(IOperation operation, ClientProfileEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.Email) && await userAuthenticationStore.IsUserExist(operation, entity.Email, entity.Id)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            await clientProfileStore.UpdateClientProfile(operation, entity);
        }

        public async Task<IEnumerable<CarEntity>> GetCarListByClient(IOperation operation, int userId)
        {
            return await carStore.GetAllByUserId(operation, userId);
        }

        public async Task<CarEntity> GetCarById(IOperation operation, int id)
        {
            if (!await carStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarIsNotExist);
            return await carStore.GetById(operation, id);
        }

        public async Task AddCar(IOperation operation, int userId, CarEntity entity)
        {
            await carStore.Add(operation, userId, entity);
        }

        public async Task UpdateCar(IOperation operation, CarEntity entity)
        {
            if (!await carStore.IsExist(operation, entity.Id)) throw new Exception(ExceptionMessage.CarIsNotExist);
            await carStore.Update(operation, entity);
        }

        public async Task DeleteCar(IOperation operation, int id)
        {
            if (!await carStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarIsNotExist);
            await carStore.Delete(operation, id);
        }

        #endregion
    }
}