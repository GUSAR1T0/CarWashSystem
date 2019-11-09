using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface IUserAuthenticationStore
    {
        #region User

        Task<bool> IsActive(IOperation operation, int id);
        Task<UserProfileEntity?> GetUserProfileById(IOperation operation, int id);
        Task<bool> IsUserExist(IOperation operation, string email);

        #endregion

        #region Company

        Task<CompanyProfileEntity?> GetCompanyProfileById(IOperation operation, int id);
        Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity);

        #endregion
        
        #region Client

        Task<ClientProfileEntity?> GetClientProfileById(IOperation operation, int id);
        Task<int?> TrySignIn(IOperation operation, ClientSignInEntity entity);
        Task<int?> TrySignUp(IOperation operation, ClientSignUpEntity entity);

        #endregion
    }
}