using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Appointment;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api")]
    [Authorize]
    public class AppointmentController : BaseApiController
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(ApplicationProperties properties, IAppointmentService appointmentService) : base(properties)
        {
            this.appointmentService = appointmentService;
        }

        /// <summary>
        /// Obtains list of appointments by client
        /// </summary>
        /// <returns>List of appointments</returns>
        [ProducesResponseType(typeof(IEnumerable<AppointmentShowItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car-wash/appointment/list")]
        public async Task<ActionResult<IEnumerable<AppointmentShowItemModel>>> GetListOfAppointmentsByClient() => await Exec(async operation =>
        {
            var (_, id) = VerifyUser(UserRole.Client);
            var appointmentList = await appointmentService.GetListOfAppointmentsByClient(operation, id);
            return appointmentList.Select(item => new AppointmentShowItemModel().ToModel(item));
        });

        /// <summary>
        /// Obtains list of appointments by car wash
        /// </summary>
        /// <param name="carWashId">Car wash ID in database</param>
        /// <returns>List of appointments</returns>
        [ProducesResponseType(typeof(IEnumerable<AppointmentShowItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car-wash/{carWashId}/appointment/list")]
        public async Task<ActionResult<IEnumerable<AppointmentShowItemModel>>> GetListOfAppointmentsByCarWash(int carWashId) => await Exec(async operation =>
        {
            var appointmentList = await appointmentService.GetListOfAppointmentsByCarWash(operation, carWashId);
            return appointmentList.Select(item => new AppointmentShowItemModel().ToModel(item));
        });

        /// <summary>
        /// Obtains extended appointment info
        /// </summary>
        /// <returns>Appointment info</returns>
        [ProducesResponseType(typeof(AppointmentShowFullItemModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car-wash/{carWashId}/appointment/{appointmentId}")]
        public async Task<ActionResult<AppointmentShowFullItemModel>> GetAppointment(int appointmentId) => await Exec(async operation =>
        {
            var appointment = await appointmentService.GetAppointment(operation, appointmentId);
            return new AppointmentShowFullItemModel().ToModel(appointment);
        });

        /// <summary>
        /// Creates appointment record
        /// </summary>
        /// <param name="model">Appointment model</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("car-wash/{carWashId}/appointment")]
        public async Task<ActionResult> AddAppointment([FromBody] AppointmentManageItemModel model) => await Exec(async operation =>
        {
            // VerifyUser(UserRole.Client);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var entity = model.ToEntity();
            await appointmentService.AddAppointment(operation, entity);
        });

        /// <summary>
        /// Updates appointment record
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <param name="model">Appointment model</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}")]
        public async Task<ActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentManageItemModel model) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Client);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            var entity = model.ToEntity(appointmentId);
            await appointmentService.UpdateAppointment(operation, role, entity);
        });

        #region Status Change

        /// <summary>
        /// Sets "Approved" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/approve")]
        public async Task<ActionResult> Approve(int appointmentId) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Both);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Approved);
        });

        /// <summary>
        /// Sets "Response Is Required" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <param name="model">Model with approved start time by company</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/response")]
        public async Task<ActionResult> RequireResponse(int appointmentId, [FromBody] AppointmentToResponseIsRequiredStatusModel model) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.ResponseIsRequired, approvedStartTime: model.StartTime);
        });

        /// <summary>
        /// Sets "In Progress" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/in-progress")]
        public async Task<ActionResult> InProgress(int appointmentId) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Company);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.InProgress);
        });

        /// <summary>
        /// Sets "Processed" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/processed")]
        public async Task<ActionResult> Processed(int appointmentId) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Company);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Processed);
        });

        /// <summary>
        /// Sets "Incident" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/incident")]
        public async Task<ActionResult> Incident(int appointmentId) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Company);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Incident);
        });

        /// <summary>
        /// Sets "Cancelled By Client" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/client-cancel")]
        public async Task<ActionResult> CancelByClient(int appointmentId) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Client);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.CancelledByClient);
        });

        /// <summary>
        /// Sets "Cancelled By Car Wash" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <param name="model">Model with comment for cancel explanation</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/company-cancel")]
        public async Task<ActionResult> CancelByCarWash(int appointmentId, [FromBody] AppointmentToCancelStatusModel model) => await Exec(async operation =>
        {
            var (role, _) = VerifyUser(UserRole.Company);
            if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ModelIsInvalid);
            await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.CancelledByCarWash, model.Comment);
        });

        #endregion
    }
}