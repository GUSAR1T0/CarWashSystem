using System.Collections.Generic;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup
{
    public class ClientLookupModel
    {
        public IEnumerable<CarBrandModelsModel> CarBrandModelsModels { get; set; } = new List<CarBrandModelsModel>();
    }
}