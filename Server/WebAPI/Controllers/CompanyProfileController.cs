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
    [Authorize]
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
        [HttpGet("car-wash/list")]
        public async Task<ActionResult<IEnumerable<CarWashFullModel>>> GetCarWashList() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            var carWashList = await companyProfileService.GetCarWashListFullByCompany(operation, id);
            var serviceList = await companyProfileService.GetServiceListByCompany(operation, id);
            return carWashList.Select(item =>
            {
                var model = new CarWashFullModel().ToModel(item);
                model.Services = serviceList.Where(service => service.CarWashId == model.Id).Select(price => new CarWashServiceModel().ToModel(price)).ToList();
                return model;
            });
        });

        /// <summary>
        /// Obtains a single company car wash model
        /// </summary>
        /// <returns>Model of company car wash</returns>
        [ProducesResponseType(typeof(CarWashFullModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [HttpDelete("car-wash/{carWashId}")]
        public async Task<ActionResult<CarWashShortModel>> DeleteCarWash(int carWashId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var carWash = await companyProfileService.DeleteCarWash(operation, carWashId);
            return new CarWashShortModel().ToModel(carWash);
        });

        #endregion

        #region Car Wash Services

        /// <summary>
        /// Obtains company car wash services
        /// </summary>
        /// <returns>List of company car wash services fully</returns>
        [ProducesResponseType(typeof(IEnumerable<CarWashServiceModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car-wash/{carWashId}/service")]
        public async Task<ActionResult<IEnumerable<CarWashServiceModel>>> GetCarWashServiceList(int carWashId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var serviceList = await companyProfileService.GetCarWashServiceListByCarWash(operation, carWashId);
            return serviceList.Select(item => new CarWashServiceModel().ToModel(item));
        });

        /// <summary>
        /// Adds a new company car wash service
        /// </summary>
        /// <returns>Model of new company car wash service shortly</returns>
        [ProducesResponseType(typeof(CarWashServiceShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("car-wash/{carWashId}/service")]
        public async Task<ActionResult<CarWashServiceShortModel>> AddCarWashService(int carWashId, [FromBody] CarWashServiceModel model) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var service = await companyProfileService.AddCarWashService(operation, carWashId, model.ToEntity());
            return new CarWashServiceShortModel().ToModel(service);
        });

        /// <summary>
        /// Updates an existed company car wash service
        /// </summary>
        /// <returns>Model of updated company car wash service shortly</returns>
        [ProducesResponseType(typeof(CarWashServiceShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/service/{serviceId}")]
        public async Task<ActionResult<CarWashServiceShortModel>> UpdateCarWashServicePrice(int serviceId, [FromBody] CarWashServiceModel model) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var service = await companyProfileService.UpdateCarWashService(operation, model.ToEntity(serviceId));
            return new CarWashServiceShortModel().ToModel(service);
        });

        /// <summary>
        /// Removes a company car wash service
        /// </summary>
        /// <returns>Model of updated company car wash service shortly</returns>
        [ProducesResponseType(typeof(CarWashServiceShortModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("car-wash/{carWashId}/service/{serviceId}")]
        public async Task<ActionResult<CarWashServiceShortModel>> DeleteCarWashServicePrice(int serviceId) => await Exec(async operation =>
        {
            VerifyUser(UserRole.Company);
            var service = await companyProfileService.DeleteCarWashService(operation, serviceId);
            return new CarWashServiceShortModel().ToModel(service);
        });

        #endregion
    }
}