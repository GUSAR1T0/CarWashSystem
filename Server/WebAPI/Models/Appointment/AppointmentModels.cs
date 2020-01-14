using System;
using System.Collections.Generic;
using System.Linq;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Appointment;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Appointment
{
    public class AppointmentShowItemModel : IEntityToModelConvertible<AppointmentShowItemEntity, AppointmentShowItemModel>
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }
        public string CarGovernmentPlate { get; set; } = "";
        public string CarWashName { get; set; } = "";
        public string CarWashLocation { get; set; } = "";
        public DateTime RequestedStartTime { get; set; }
        public DateTime? ApprovedStartTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public IEnumerable<CarWashServiceModel> CarWashServices { get; set; } = new List<CarWashServiceModel>();

        public AppointmentShowItemModel ToModel(AppointmentShowItemEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            CarModelId = entity.CarModelId;
            CarGovernmentPlate = entity.CarGovernmentPlate;
            CarWashName = entity.CarWashName;
            CarWashLocation = entity.CarWashLocation;
            RequestedStartTime = entity.RequestedStartTime;
            ApprovedStartTime = entity.ApprovedStartTime;
            Status = entity.Status;
            CarWashServices = entity.CarWashServices.Select(service => new CarWashServiceModel().ToModel(service));
            return this;
        }
    }

    public class AppointmentShowItemWithHistoryModel : IEntityToModelConvertible<AppointmentShowItemWithHistoryEntity, AppointmentShowItemWithHistoryModel>
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }
        public string CarGovernmentPlate { get; set; } = "";
        public string CarWashName { get; set; } = "";
        public string CarWashLocation { get; set; } = "";
        public DateTime RequestedStartTime { get; set; }
        public DateTime? ApprovedStartTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public IEnumerable<CarWashServiceModel> CarWashServices { get; set; } = new List<CarWashServiceModel>();
        public IEnumerable<AppointmentHistoryModel> History { get; set; } = new List<AppointmentHistoryModel>();

        public AppointmentShowItemWithHistoryModel ToModel(AppointmentShowItemWithHistoryEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            CarModelId = entity.CarModelId;
            CarGovernmentPlate = entity.CarGovernmentPlate;
            CarWashName = entity.CarWashName;
            CarWashLocation = entity.CarWashLocation;
            RequestedStartTime = entity.RequestedStartTime;
            ApprovedStartTime = entity.ApprovedStartTime;
            Status = entity.Status;
            CarWashServices = entity.CarWashServices.Select(service => new CarWashServiceModel().ToModel(service));
            History = entity.History.Select(item => new AppointmentHistoryModel().ToModel(item));
            return this;
        }
    }

    public class AppointmentManageItemModel : IModelToEntityConvertible<AppointmentManageItemEntity>, IModelToEntityWithIdentifierConvertible<AppointmentManageItemEntity>
    {
        public int CarId { get; set; }
        public int CarWashId { get; set; }
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