using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Stores.Interfaces;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;

namespace VXDesign.Store.CarWashSystem.Server.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentStore appointmentStore;

        public AppointmentService(IAppointmentStore appointmentStore)
        {
            this.appointmentStore = appointmentStore;
        }

        public async Task<IEnumerable<AppointmentShowItemEntity>> GetListOfAppointmentsByClient(IOperation operation, int userId)
        {
            var appointments = (await appointmentStore.GetListByClient(operation, userId)).ToList();
            foreach (var appointment in appointments)
            {
                appointment.CarWashServices = await appointmentStore.GetServicesByAppointment(operation, appointment.Id);
            }

            return appointments;
        }

        public async Task<IEnumerable<AppointmentShowItemEntity>> GetListOfAppointmentsByCarWash(IOperation operation, int carWashId)
        {
            var appointments = (await appointmentStore.GetListByCarWash(operation, carWashId)).ToList();
            foreach (var appointment in appointments)
            {
                appointment.CarWashServices = await appointmentStore.GetServicesByAppointment(operation, appointment.Id);
            }

            return appointments;
        }

        public async Task<AppointmentShowItemWithHistoryEntity> GetAppointment(IOperation operation, int appointmentId)
        {
            if (!await appointmentStore.IsExist(operation, appointmentId)) throw new Exception(ExceptionMessage.AppointmentIsNotExist);

            var appointment = await appointmentStore.Get(operation, appointmentId);
            appointment.CarWashServices = await appointmentStore.GetServicesByAppointment(operation, appointmentId);
            appointment.History = await appointmentStore.GetHistoryRecords(operation, appointmentId);
            return appointment;
        }

        public async Task AddAppointment(IOperation operation, AppointmentManageItemEntity entity)
        {
            // TODO: Validate appointment time
            var appointmentId = await appointmentStore.Add(operation, entity);
            await appointmentStore.AddHistoryRecord(operation, appointmentId, "Appointment was created");
        }

        public async Task UpdateAppointment(IOperation operation, UserRole role, AppointmentManageItemEntity entity)
        {
            if (!await appointmentStore.IsExist(operation, entity.Id)) throw new Exception(ExceptionMessage.AppointmentIsNotExist);

            // TODO: Validate appointment time
            switch (role)
            {
                case UserRole.Client:
                    await appointmentStore.UpdateAsClient(operation, entity);
                    break;
                case UserRole.Company:
                    await appointmentStore.UpdateAsCompany(operation, entity);
                    break;
                case UserRole.Both:
                default:
                    break;
            }

            await appointmentStore.AddHistoryRecord(operation, entity.Id, $"Appointment was updated by '{role.GetUserRoleName()}'");
        }

        public async Task ChangeAppointmentStatus(IOperation operation, UserRole role, int appointmentId, AppointmentStatus status, string? comment = null)
        {
            if (!await appointmentStore.IsExist(operation, appointmentId)) throw new Exception(ExceptionMessage.AppointmentIsNotExist);

            if (status == AppointmentStatus.Approved)
            {
                var appointment = await appointmentStore.Get(operation, appointmentId);
                if (appointment.ApprovedStartTime == null)
                {
                    await appointmentStore.UpdateAsCompany(operation, new AppointmentManageItemEntity
                    {
                        Id = appointmentId,
                        StartTime = appointment.RequestedStartTime
                    });
                }
            }

            await appointmentStore.ChangeStatus(operation, appointmentId, status);
            await appointmentStore.AddHistoryRecord(operation, appointmentId,
                $"Appointment status was changed by '{role.GetUserRoleName()}'{(string.IsNullOrWhiteSpace(comment) ? "" : $": {comment}")}");
        }
    }
}