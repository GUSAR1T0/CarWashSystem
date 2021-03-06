using System.Collections.Generic;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup
{
    public class CompanyLookupModel
    {
        public IEnumerable<CarBrandModelsModel> CarBrandModelsModels { get; set; } = new List<CarBrandModelsModel>();
        public IEnumerable<EnumRowModel> AppointmentStatusModels { get; set; }
    }
}