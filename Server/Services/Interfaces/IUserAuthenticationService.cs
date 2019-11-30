using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        #region User

        Task<bool> IsActivated(IOperation operation, int id);

        #endregion

        #region Company

        Task<CompanyAuthenticationProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<CompanyAuthenticationProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity);
        Task<CompanyAuthenticationProfileEntity?> GetCompanyProfile(IOperation operation, int id);

        #endregion

        #region Client

        Task<ClientAuthenticationProfileEntity?> TrySignIn(IOperation operation, ClientSignInEntity entity);
        Task<ClientAuthenticationProfileEntity?> TrySignUp(IOperation operation, ClientSignUpEntity entity);
        Task<Guid> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity);
        Task<ClientAuthenticationProfileEntity?> TryExternalSignIn(IOperation operation, Guid token);
        Task<ClientAuthenticationProfileEntity?> GetClientProfile(IOperation operation, int id);

        #endregion
    }
}