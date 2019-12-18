using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class CarWashToClientService : ICarWashToClientService
    {
        private readonly ICarWashStore carWashStore;
        private readonly ICarWashServiceStore carWashServiceStore;

        public CarWashToClientService(ICarWashStore carWashStore, ICarWashServiceStore carWashServiceStore)
        {
            this.carWashStore = carWashStore;
            this.carWashServiceStore = carWashServiceStore;
        }

        public async Task<IEnumerable<CarWashFullEntity>> GetCarWashList(IOperation operation)
        {
            return await carWashStore.GetAll(operation);
        }

        public async Task<CarWashFullEntity> GetCarWashById(IOperation operation, int id)
        {
            if (!await carWashStore.IsExist(operation, id)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashStore.GetById(operation, id);
        }

        public async Task<IEnumerable<CarWashServiceEntity>> GetCarWashServiceListByCarWash(IOperation operation, int carWashId)
        {
            if (!await carWashStore.IsExist(operation, carWashId)) throw new Exception(ExceptionMessage.CarWashIsNotExist);
            return await carWashServiceStore.GetListByCarWash(operation, carWashId);
        }
    }
}