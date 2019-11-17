using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        #region User

        Task<bool> IsActive(IOperation operation, int id);

        #endregion

        #region Company

        Task<CompanyProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<CompanyProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity);
        Task<CompanyProfileEntity?> GetCompanyProfile(IOperation operation, int id);

        #endregion

        #region Client

        Task<ClientProfileEntity?> TrySignIn(IOperation operation, ClientSignInEntity entity);
        Task<ClientProfileEntity?> TrySignUp(IOperation operation, ClientSignUpEntity entity);
        Task<ClientProfileEntity?> GetClientProfile(IOperation operation, int id);

        #endregion
    }
}