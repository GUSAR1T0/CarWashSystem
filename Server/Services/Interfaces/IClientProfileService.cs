using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.Services.Interfaces
{
    public interface IClientProfileService
    {
        #region Client Profile

        Task<ClientProfileEntity> GetClientFullProfile(IOperation operation, int id);
        Task UpdateClientFullProfile(IOperation operation, ClientProfileEntity entity);

        #endregion

        #region Cars

        Task<IEnumerable<CarEntity>> GetCarListByClient(IOperation operation, int userId);
        Task<CarEntity> GetCarById(IOperation operation, int id);
        Task AddCar(IOperation operation, int userId, CarEntity entity);
        Task UpdateCar(IOperation operation, CarEntity entity);
        Task DeleteCar(IOperation operation, int id);

        #endregion
    }
}