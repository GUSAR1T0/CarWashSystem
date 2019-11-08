using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class CompanyAuthenticationService : ICompanyAuthenticationService
    {
        private readonly ICompanyAuthenticationStore companyAuthenticationStore;

        public CompanyAuthenticationService(ICompanyAuthenticationStore companyAuthenticationStore)
        {
            this.companyAuthenticationStore = companyAuthenticationStore;
        }

        public async Task<bool> IsActive(IOperation operation, int id) => await companyAuthenticationStore.IsActive(operation, id);

        public async Task<CompanyProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity)
        {
            var id = await companyAuthenticationStore.TrySignIn(operation, entity);
            return await companyAuthenticationStore.GetProfileById(operation, id ?? throw new Exception("Authentication process failed: couldn't sign in you as company representative"));
        }

        public async Task<CompanyProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity)
        {
            var id = await companyAuthenticationStore.TrySignUp(operation, entity);
            return await companyAuthenticationStore.GetProfileById(operation, id ?? throw new Exception("Authentication process failed: couldn't sign up you as company representative"));
        }

        public async Task<CompanyProfileEntity?> GetProfile(IOperation operation, int id) => await companyAuthenticationStore.GetProfileById(operation, id);
    }
}