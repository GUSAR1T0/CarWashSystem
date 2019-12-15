using System.Collections.Generic;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile
{
    public class CarBrandEntity : EnumRowEntity
    {
    }

    public class CarBrandModelEntity : EnumRowEntity
    {
        public int BrandId { get; set; }
    }

    public class CarBrandModelsEntity : EnumRowEntity
    {
        public IEnumerable<EnumRowEntity> Models { get; set; }
    }
}