using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/company/profile")]
    public class CompanyProfileController : BaseApiController
    {
        private readonly ICompanyProfileService companyProfileService;

        public CompanyProfileController(ApplicationProperties properties, ICompanyProfileService companyProfileService) : base(properties)
        {
            this.companyProfileService = companyProfileService;
        }

        #region Company Full Profile

        /// <summary>
        /// Obtains company profile
        /// </summary>
        /// <returns>Model of company profile</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CompanyProfileModel>> GetCompanyFullProfile() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            var profile = await companyProfileService.GetCompanyFullProfile(operation, id);
            return new CompanyProfileModel().ToModel(profile);
        });

        /// <summary>
        /// Updates company profile
        /// </summary>
        /// <param name="companyProfileModel"></param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateCompanyFullProfile([FromBody] CompanyProfileModel companyProfileModel) => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var entity = companyProfileModel.ToEntity(id);
            await companyProfileService.UpdateCompanyFullProfile(operation, entity);
        });

        #endregion

        #region Car Wash

        /// <summary>
        /// Obtains company car washes
        /// </summary>
        /// <returns>List of company car washes fully</returns>
        [ProducesResponseType(typeof(IEnumerable<CarWashFullModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("car-wash/list")]
        public async Task<ActionResult<IEnumerable<CarWashFullModel>>> GetCarWashList() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            var list = await companyProfileService.GetCarWashListFullByCompany(operation, id);
            return list.Select(item => new CarWashFullModel().ToModel(item));
        });

        /// <summary>
        /// Obtains a single company car wash model
        /// </summary>
        /// <returns>Model of company car wash</returns>
        [ProducesResponseType(typeof(CarWashFullModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("car-wash/{carWashId}")]
        public async Task<ActionResult<CarWashFullModel>> GetCarWash(int carWashId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var entity = await companyProfileService.GetCarWashById(operation, carWashId);
            return new CarWashFullModel().ToModel(entity);
        });

        /// <summary>
        /// Adds a new company car wash
        /// </summary>
        /// <returns>Model of new company car wash shortly</returns>
        [ProducesResponseType(typeof(CarWashShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("car-wash")]
        public async Task<ActionResult<CarWashShortModel>> AddCarWash([FromBody] CarWashFullModel model) => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var carWash = await companyProfileService.AddCarWash(operation, id, model.ToEntity());
            return new CarWashShortModel().ToModel(carWash);
        });

        /// <summary>
        /// Updates an existed company car wash
        /// </summary>
        /// <returns>Model of updated company car wash shortly</returns>
        [ProducesResponseType(typeof(CarWashShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPut("car-wash/{carWashId}")]
        public async Task<ActionResult<CarWashShortModel>> UpdateCarWash(int carWashId, [FromBody] CarWashFullModel model) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var carWash = await companyProfileService.UpdateCarWash(operation, model.ToEntity(carWashId));
            return new CarWashShortModel().ToModel(carWash);
        });

        /// <summary>
        /// Removes a company car wash
        /// </summary>
        /// <returns>Model of updated company car wash shortly</returns>
        [ProducesResponseType(typeof(CarWashShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpDelete("car-wash/{carWashId}")]
        public async Task<ActionResult<CarWashShortModel>> DeleteCarWash(int carWashId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var carWash = await companyProfileService.DeleteCarWash(operation, carWashId);
            return new CarWashShortModel().ToModel(carWash);
        });

        #endregion
    }
}