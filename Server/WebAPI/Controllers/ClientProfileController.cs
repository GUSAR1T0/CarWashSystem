using System;
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
    }
}