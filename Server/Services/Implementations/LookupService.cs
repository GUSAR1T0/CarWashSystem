using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class LookupService : ILookupService
    {
        private readonly ICarBrandModelStore carBrandModelStore;

        public LookupService(ICarBrandModelStore carBrandModelStore)
        {
            this.carBrandModelStore = carBrandModelStore;
        }

        public async Task<IEnumerable<CarBrandModelsEntity>> GetCarBrandModels(IOperation operation)
        {
            var brands = await carBrandModelStore.GetCarBrands(operation);
            var models = await carBrandModelStore.GetCarBrandModels(operation);
            return brands.Select(brand => new CarBrandModelsEntity
            {
                Id = brand.Id,
                Name = brand.Name,
                Models = models.Where(model => model.BrandId == brand.Id).ToList()
            }).ToList();
        }
    }
}