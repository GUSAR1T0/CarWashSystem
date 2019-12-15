using System;
using System.Collections.Generic;
using System.Linq;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup
{
    public class CarBrandModelsModel : EnumRowModel, IEntityToModelConvertible<CarBrandModelsEntity, CarBrandModelsModel>
    {
        public IEnumerable<EnumRowModel> Models { get; set; }

        public CarBrandModelsModel ToModel(CarBrandModelsEntity entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            Name = entity.Name;
            Models = entity.Models.Select(model => new EnumRowModel().ToModel(model)).ToList();
            return this;
        }
    }
}