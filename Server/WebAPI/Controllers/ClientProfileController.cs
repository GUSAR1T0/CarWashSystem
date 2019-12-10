using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.ClientProfile;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/client/profile")]
    [Authorize]
    public class ClientProfileController : BaseApiController
    {
        private readonly IClientProfileService clientProfileService;

        public ClientProfileController(ApplicationProperties properties, IClientProfileService clientProfileService) : base(properties)
        {
            this.clientProfileService = clientProfileService;
        }

        #region Client Full Profile

        /// <summary>
        /// Obtains client profile
        /// </summary>
        /// <returns>Model of client profile</returns>
        [ProducesResponseType(typeof(ClientProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<ClientProfileModel>> GetClientFullProfile() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Client);
            var profile = await clientProfileService.GetClientFullProfile(operation, id);
            return new ClientProfileModel().ToModel(profile);
        });

        /// <summary>
        /// Updates client profile
        /// </summary>
        /// <param name="clientProfileModel">Client profile model for update</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<ActionResult> UpdateClientFullProfile([FromBody] ClientProfileModel clientProfileModel) => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Client);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var entity = clientProfileModel.ToEntity(id);
            await clientProfileService.UpdateClientFullProfile(operation, entity);
        });

        #endregion

        #region Cars

        /// <summary>
        /// Obtains client cars
        /// </summary>
        /// <returns>List of client cars</returns>
        [ProducesResponseType(typeof(IEnumerable<CarModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car/list")]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarWashList() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Client);
            var carList = await clientProfileService.GetCarListByClient(operation, id);
            return carList.Select(item => new CarModel().ToModel(item));
        });

        /// <summary>
        /// Obtains a single client car model
        /// </summary>
        /// <param name="carId">Car ID in database</param>
        /// <returns>Model of client car fully</returns>
        [ProducesResponseType(typeof(CarModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<CarModel>> GetCar(int carId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            var entity = await clientProfileService.GetCarById(operation, carId);
            return new CarModel().ToModel(entity);
        });

        /// <summary>
        /// Adds a new client car
        /// </summary>
        /// <param name="model">Car model for adding</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("car")]
        public async Task<ActionResult> AddCar([FromBody] CarModel model) => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Client);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            await clientProfileService.AddCar(operation, id, model.ToEntity());
        });

        /// <summary>
        /// Updates an existed client car
        /// </summary>
        /// <param name="carId">Car ID in database</param>
        /// <param name="model">Car model for update</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car/{carId}")]
        public async Task<ActionResult> UpdateCar(int carId, [FromBody] CarModel model) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            await clientProfileService.UpdateCar(operation, model.ToEntity(carId));
        });

        /// <summary>
        /// Removes a client car
        /// </summary>
        /// <param name="carId">Car ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("car/{carId}")]
        public async Task<ActionResult> DeleteCar(int carId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Client);
            await clientProfileService.DeleteCar(operation, carId);
        });

        #endregion
    }
}