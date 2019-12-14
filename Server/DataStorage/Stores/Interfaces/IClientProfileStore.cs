using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces
{
    public interface IClientProfileStore
    {
        Task<ClientProfileEntity> GetClientProfileById(IOperation operation, int id);
        Task UpdateClientProfile(IOperation operation, ClientProfileEntity entity);
    }
}