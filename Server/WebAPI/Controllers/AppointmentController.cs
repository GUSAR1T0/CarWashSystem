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
        [ProducesResponseType(typeof(AppointmentShowItemWithHistoryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("car-wash/{carWashId}/appointment/{appointmentId}")]
        public async Task<ActionResult<AppointmentShowItemWithHistoryModel>> GetAppointment(int appointmentId) => await Exec(async operation =>
        {
            var appointment = await appointmentService.GetAppointment(operation, appointmentId);
            return new AppointmentShowItemWithHistoryModel().ToModel(appointment);
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
            VerifyUser(UserRole.Client);
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
            var (role, _) = VerifyUser(UserRole.Both);
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
        public async Task<ActionResult> Approve(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Both);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Approved));
        }

        /// <summary>
        /// Sets "Response Is Required" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/response")]
        public async Task<ActionResult> RequireResponse(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.ResponseIsRequired));
        }

        /// <summary>
        /// Sets "In Progress" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/in-progress")]
        public async Task<ActionResult> InProgress(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.InProgress));
        }

        /// <summary>
        /// Sets "Processed" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/processed")]
        public async Task<ActionResult> Processed(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Processed));
        }

        /// <summary>
        /// Sets "Incident" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/incident")]
        public async Task<ActionResult> Incident(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Incident));
        }

        /// <summary>
        /// Sets "Closed" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/close")]
        public async Task<ActionResult> Close(int appointmentId)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.Closed));
        }

        /// <summary>
        /// Sets "Cancelled By Client" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <param name="comment">Comment for cancel explanation</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/client-cancel")]
        public async Task<ActionResult> CancelByClient(int appointmentId, [FromBody] string comment)
        {
            var (role, _) = VerifyUser(UserRole.Client);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.CancelledByClient, comment));
        }

        /// <summary>
        /// Sets "Cancelled By Car Wash" status to appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID in database</param>
        /// <param name="comment">Comment for cancel explanation</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("car-wash/{carWashId}/appointment/{appointmentId}/company-cancel")]
        public async Task<ActionResult> CancelByCarWash(int appointmentId, [FromBody] string comment)
        {
            var (role, _) = VerifyUser(UserRole.Company);
            return await Exec(async operation => await appointmentService.ChangeAppointmentStatus(operation, role, appointmentId, AppointmentStatus.CancelledByCarWash, comment));
        }

        #endregion
    }
}