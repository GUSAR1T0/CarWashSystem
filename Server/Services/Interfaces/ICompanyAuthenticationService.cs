using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface ICompanyAuthenticationService
    {
        Task<bool> IsActive(IOperation operation, int id);
        Task<CompanyProfileEntity?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<CompanyProfileEntity?> TrySignUp(IOperation operation, CompanySignUpEntity entity);
        Task<CompanyProfileEntity?> GetProfile(IOperation operation, int id);
    }
}