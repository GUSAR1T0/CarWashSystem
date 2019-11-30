using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserAuthenticationStore userAuthenticationStore;

        public UserAuthenticationService(IUserAuthenticationStore userAuthenticationStore)
        {
            this.userAuthenticationStore = userAuthenticationStore;
        }

        #region User

        public async Task<bool> IsActivated(IOperation operation, int id) => await userAuthenticationStore.IsUserActivated(operation, id);

        #endregion

        #region Company

        public async Task<CompanyAuthenticationProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            var id = await userAuthenticationStore.TrySignIn(operation, entity);
            return await userAuthenticationStore.GetCompanyProfileById(operation,
                id ?? throw new Exception(ExceptionMessage.CompanySignInFailedDueToInactiveUserOrUnsuitableData));
        }

        public async Task<CompanyAuthenticationProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            var id = await userAuthenticationStore.TrySignUp(operation, entity);
            return await userAuthenticationStore.GetCompanyProfileById(operation,
                id ?? throw new Exception(ExceptionMessage.CompanySignUpFailedDueToInactiveUserOrUnsuitableData));
        }

        public async Task<CompanyAuthenticationProfileEntity?> GetCompanyProfile(IOperation operation, int id) => await userAuthenticationStore.GetCompanyProfileById(operation, id);

        #endregion

        #region Client

        public async Task<ClientAuthenticationProfileEntity?> TrySignIn(IOperation operation, ClientSignInEntity entity)
        {
            var id = await userAuthenticationStore.TrySignIn(operation, entity);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception(ExceptionMessage.ClientSignInFailedDueToInactiveUserOrUnsuitableData));
        }

        public async Task<ClientAuthenticationProfileEntity?> TrySignUp(IOperation operation, ClientSignUpEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email)) throw new Exception(ExceptionMessage.EmailHasAlreadyExist);
            var id = await userAuthenticationStore.TrySignUp(operation, entity);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception(ExceptionMessage.ClientSignUpFailedDueToInactiveUserOrUnsuitableData));
        }

        public async Task<Guid> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity)
        {
            return await userAuthenticationStore.TryExternalSignIn(operation, entity) ??
                     throw new Exception(ExceptionMessage.ExternalClientSignUpFailedDueToInactiveUserOrUnsuitableData);
        }

        public async Task<ClientAuthenticationProfileEntity?> TryExternalSignIn(IOperation operation, Guid token)
        {
            var id = await userAuthenticationStore.TryExternalSignIn(operation, token);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception(ExceptionMessage.ExternalClientSignUpFailedDueToInactiveUserOrUnsuitableData));
        }

        public async Task<ClientAuthenticationProfileEntity?> GetClientProfile(IOperation operation, int id) => await userAuthenticationStore.GetClientProfileById(operation, id);

        #endregion
    }
}