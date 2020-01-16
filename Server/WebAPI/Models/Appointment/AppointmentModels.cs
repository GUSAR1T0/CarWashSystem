using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Appointment
{
    public class AppointmentShowItemModel : IEntityToModelConvertible<AppointmentShowItemEntity, AppointmentShowItemModel>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int CarModelId { get; set; }
        public string CarGovernmentPlate { get; set; } = "";
        public string CarWashName { get; set; } = "";
        public string CarWashLocation { get; set; } = "";
        public string RequestedStartDate { get; set; } = "";
        public string RequestedStartTime { get; set; } = "";
        public string? ApprovedStartDate { get; set; }
        public string? ApprovedStartTime { get; set; }
        public AppointmentStatus Status { get; set; }

        public AppointmentShowItemModel ToModel(AppointmentShowItemEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            FullName = entity.FirstName + " " + entity.LastName;
            CarModelId = entity.CarModelId;
            CarGovernmentPlate = entity.CarGovernmentPlate;
            CarWashName = entity.CarWashName;
            CarWashLocation = entity.CarWashLocation;
            RequestedStartDate = entity.RequestedStartTime.ToString(Formatters.DateFormat);
            RequestedStartTime = entity.RequestedStartTime.ToString(Formatters.TimeFormat);
            ApprovedStartDate = entity.ApprovedStartTime?.ToString(Formatters.DateFormat);
            ApprovedStartTime = entity.ApprovedStartTime?.ToString(Formatters.TimeFormat);
            Status = entity.Status;
            return this;
        }
    }

    public class AppointmentShowFullItemModel : IEntityToModelConvertible<AppointmentShowFullItemEntity, AppointmentShowFullItemModel>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int CarModelId { get; set; }
        public string CarGovernmentPlate { get; set; } = "";
        public string CarWashName { get; set; } = "";
        public string CarWashLocation { get; set; } = "";
        public string RequestedStartDate { get; set; } = "";
        public string RequestedStartTime { get; set; } = "";
        public string? ApprovedStartDate { get; set; }
        public string? ApprovedStartTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public IEnumerable<CarWashServiceModel> CarWashServices { get; set; } = new List<CarWashServiceModel>();
        public IEnumerable<AppointmentHistoryModel> History { get; set; } = new List<AppointmentHistoryModel>();

        public AppointmentShowFullItemModel ToModel(AppointmentShowFullItemEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            FullName = entity.FirstName + " " + entity.LastName;
            CarModelId = entity.CarModelId;
            CarGovernmentPlate = entity.CarGovernmentPlate;
            CarWashName = entity.CarWashName;
            CarWashLocation = entity.CarWashLocation;
            RequestedStartDate = entity.RequestedStartTime.ToString(Formatters.DateFormat);
            RequestedStartTime = entity.RequestedStartTime.ToString(Formatters.TimeFormat);
            ApprovedStartDate = entity.ApprovedStartTime?.ToString(Formatters.DateFormat);
            ApprovedStartTime = entity.ApprovedStartTime?.ToString(Formatters.TimeFormat);
            Status = entity.Status;
            CarWashServices = entity.CarWashServices.Select(service => new CarWashServiceModel().ToModel(service));
            History = entity.History.Select(item => new AppointmentHistoryModel().ToModel(item));
            return this;
        }
    }

    public class AppointmentManageItemModel : IModelToEntityConvertible<AppointmentManageItemEntity>, IModelToEntityWithIdentifierConvertible<AppointmentManageItemEntity>
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public int CarWashId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public IEnumerable<int> CarWashServiceIds { get; set; } = new List<int>();

        public AppointmentManageItemEntity ToEntity() => new AppointmentManageItemEntity
        {
            CarId = CarId,
            CarWashId = CarWashId,
            StartTime = StartTime,
            CarWashServiceIds = CarWashServiceIds
        };

        public AppointmentManageItemEntity ToEntity(int id)
        {
            var entity = ToEntity();
            entity.Id = id;
            return entity;
        }
    }
}