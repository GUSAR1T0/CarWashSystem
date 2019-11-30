using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface ICompanyProfileStore
    {
        Task<CompanyProfileEntity> GetCompanyProfileById(IOperation operation, int id);
        Task UpdateCompanyProfile(IOperation operation, CompanyProfileEntity entity);
    }
}