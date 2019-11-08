using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICompanyAuthenticationStore
    {
        Task<bool> IsActive(IOperation operation, int id);
        Task<CompanyProfileEntity?> GetProfileById(IOperation operation, int id);
        Task<int?> TrySignIn(IOperation operation, CompanySignInEntity entity);
        Task<int?> TrySignUp(IOperation operation, CompanySignUpEntity entity);
    }
}