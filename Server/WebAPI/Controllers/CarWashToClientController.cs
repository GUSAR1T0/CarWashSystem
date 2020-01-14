using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CarWashToClient;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/client/car-wash")]
    [Authorize]
    public class CarWashToClientController : BaseApiController
    {
        private readonly ICarWashToClientService carWashToClientService;

        public CarWashToClientController(ApplicationProperties properties, ICarWashToClientService carWashToClientService) : base(properties)
        {
            this.carWashToClientService = carWashToClientService;
        }

        /// <summary>
        /// Obtains all car washes
        /// </summary>
        /// <returns>List of car washes shortly</returns>
        [ProducesResponseType(typeof(IEnumerable<CarWashToClientShortModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CarWashToClientShortModel>>> GetCarWashList() => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            var carWashList = await carWashToClientService.GetCarWashList(operation);
            return carWashList.Select(item => new CarWashToClientShortModel().ToModel(item));
        });

        /// <summary>
        /// Obtains a single car wash model
        /// </summary>
        /// <param name="carWashId">Car wash ID in database</param>
        /// <returns>Model of car wash fully</returns>
        [ProducesResponseType(typeof(CarWashToClientFullModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{carWashId}")]
        public async Task<ActionResult<CarWashToClientFullModel>> GetCarWash(int carWashId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            var entity = await carWashToClientService.GetCarWashById(operation, carWashId);
            var model = new CarWashToClientFullModel().ToModel(entity);
            var services = await carWashToClientService.GetCarWashServiceListByCarWash(operation, carWashId);
            model.Services = services.Select(item => new CarWashServiceModel().ToModel(item)).ToList();
            return model;
        });
    }
}