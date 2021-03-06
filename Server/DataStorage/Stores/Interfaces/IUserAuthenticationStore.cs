using System;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface IUserAuthenticationStore
    {
        #region User

        Task<bool> IsUserActivated(IOperation operation, int id);
        Task<bool> IsUserExist(IOperation operation, string email, int? id = null);

        #endregion

        #region Company

        Task<CompanyAuthenticationProfileEntity?> GetCompanyProfileById(IOperation operation, int id);
        Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity);

        #endregion

        #region Client

        Task<ClientAuthenticationProfileEntity?> GetClientProfileById(IOperation operation, int id);
        Task<int?> TrySignIn(IOperation operation, ClientSignInEntity entity);
        Task<int?> TrySignUp(IOperation operation, ClientSignUpEntity entity);
        Task<Guid?> TryExternalSignIn(IOperation operation, ExternalClientSignInEntity entity);
        Task<int?> TryExternalSignIn(IOperation operation, Guid token);

        #endregion
    }
}