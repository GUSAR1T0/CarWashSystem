using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Lookup;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/lookup")]
    [Authorize]
    public class LookupController : BaseApiController
    {
        private readonly ILookupService lookupService;

        public LookupController(ApplicationProperties properties, ILookupService lookupService) : base(properties)
        {
            this.lookupService = lookupService;
        }

        /// <summary>
        /// Returns lookup for client app
        /// </summary>
        /// <returns>Client lookup object</returns>
        [ProducesResponseType(typeof(ClientLookupModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("client")]
        public async Task<ActionResult<ClientLookupModel>> GetClientLookup() => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            var carBrandModels = await lookupService.GetCarBrandModels(operation);
            return new ClientLookupModel
            {
                CarBrandModelsModels = carBrandModels.Select(model => new CarBrandModelsModel().ToModel(model)).ToList(),
                AppointmentStatusModels = lookupService.GetAppointmentStatuses().Select(entity => new EnumRowModel().ToModel(entity))
            };
        });

        /// <summary>
        /// Returns lookup for company app
        /// </summary>
        /// <returns>Company lookup object</returns>
        [ProducesResponseType(typeof(CompanyLookupModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("company")]
        public async Task<ActionResult<CompanyLookupModel>> GetCompanyLookup() => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var carBrandModels = await lookupService.GetCarBrandModels(operation);
            return new CompanyLookupModel
            {
                CarBrandModelsModels = carBrandModels.Select(model => new CarBrandModelsModel().ToModel(model)).ToList(),
                AppointmentStatusModels = lookupService.GetAppointmentStatuses().Select(entity => new EnumRowModel().ToModel(entity))
            };
        });
    }
}