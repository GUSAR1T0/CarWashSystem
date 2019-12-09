using System;
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

        public ClientProfileService(IUserAuthenticationStore userAuthenticationStore, IClientProfileStore clientProfileStore)
        {
            this.userAuthenticationStore = userAuthenticationStore;
            this.clientProfileStore = clientProfileStore;
        }

        #region Client Profile

        public async Task<ClientProfileEntity> GetClientFullProfile(IOperation operation, int id)
        {
            return await clientProfileStore.GetClientProfileById(operation, id);
        }

        public async Task UpdateClientFullProfile(IOperation operation, ClientProfileEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email, entity.Id)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            await clientProfileStore.UpdateClientProfile(operation, entity);
        }

        #endregion
        
    }
}