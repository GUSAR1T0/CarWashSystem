using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
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

        public IEnumerable<EnumRowEntity> GetAppointmentStatuses() => Enum.GetValues(typeof(AppointmentStatus))
            .Cast<AppointmentStatus>()
            .Select(status =>
            {
                var description = status.GetType().GetMember(status.ToString())[0].GetCustomAttributes<DescriptionAttribute>(false).FirstOrDefault()?.Description;
                return new EnumRowEntity
                {
                    Id = (byte) status,
                    Name = description ?? string.Empty
                };
            });
    }
}