using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
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

        public async Task<bool> IsActive(IOperation operation, int id) => await userAuthenticationStore.IsActive(operation, id);

        #endregion

        #region Company

        public async Task<CompanyProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            var id = await userAuthenticationStore.TrySignIn(operation, entity);
            return await userAuthenticationStore.GetCompanyProfileById(operation,
                id ?? throw new Exception("Authentication process failed: you couldn't be signed in as company representative due to inactive user is or data is not suitable"));
        }

        public async Task<CompanyProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email)) throw new Exception("Authentication process failed: such email is already used by the system");
            var id = await userAuthenticationStore.TrySignUp(operation, entity);
            return await userAuthenticationStore.GetCompanyProfileById(operation,
                id ?? throw new Exception("Authentication process failed: you couldn't be signed up as company representative due to inactive user is or data is not suitable"));
        }

        public async Task<CompanyProfileEntity?> GetCompanyProfile(IOperation operation, int id) => await userAuthenticationStore.GetCompanyProfileById(operation, id);

        #endregion

        #region Client

        public async Task<ClientProfileEntity?> TrySignIn(IOperation operation, ClientSignInEntity entity)
        {
            var id = await userAuthenticationStore.TrySignIn(operation, entity);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception("Authentication process failed: you couldn't be signed in as client due to inactive user is or data is not suitable"));
        }

        public async Task<ClientProfileEntity?> TrySignUp(IOperation operation, ClientSignUpEntity entity)
        {
            if (await userAuthenticationStore.IsUserExist(operation, entity.Email)) throw new Exception("Authentication process failed: such email is already used by the system");
            var id = await userAuthenticationStore.TrySignUp(operation, entity);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception("Authentication process failed: you couldn't be signed up as client due to inactive user is or data is not suitable"));
        }

        public async Task<Guid> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity)
        {
            return await userAuthenticationStore.TryExternalSignIn(operation, entity) ??
                     throw new Exception("Authentication process failed: you couldn't be signed in as client due to inactive user is or data is not suitable");
        }

        public async Task<ClientProfileEntity?> TryExternalSignIn(IOperation operation, Guid token)
        {
            var id = await userAuthenticationStore.TryExternalSignIn(operation, token);
            return await userAuthenticationStore.GetClientProfileById(operation,
                id ?? throw new Exception("Authentication process failed: you couldn't be signed in as client due to inactive user is or token is not suitable"));
        }

        public async Task<ClientProfileEntity?> GetClientProfile(IOperation operation, int id) => await userAuthenticationStore.GetClientProfileById(operation, id);

        #endregion
    }
}