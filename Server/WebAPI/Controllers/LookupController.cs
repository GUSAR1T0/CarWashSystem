using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api")]
    [Authorize]
    public class LookupController : BaseApiController
    {
        private readonly ILookupService lookupService;

        public LookupController(ApplicationProperties properties, ILookupService lookupService) : base(properties)
        {
            this.lookupService = lookupService;
        }

        [HttpGet("client/lookup")]
        public async Task<ActionResult<ClientLookupModel>> GetClientLookup() => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            var carBrandModels = await lookupService.GetCarBrandModels(operation);
            return new ClientLookupModel
            {
                CarBrandModelsModels = carBrandModels.Select(model => new CarBrandModelsModel().ToModel(model)).ToList()
            };
        });
    }
}