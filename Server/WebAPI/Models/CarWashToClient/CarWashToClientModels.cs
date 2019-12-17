using System;
using System.Collections.Generic;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CarWashToClient
{
    public class CarWashToClientFullModel : IEntityToModelConvertible<CarWashFullEntity, CarWashToClientFullModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Location { get; set; } = "";
        public decimal CoordinateX { get; set; }
        public decimal CoordinateY { get; set; }
        public string? Description { get; set; }
        public bool HasCafe { get; set; }
        public bool HasRestZone { get; set; }
        public bool HasParking { get; set; }
        public bool HasWC { get; set; }
        public bool HasCardPayment { get; set; }
        public CarWashWorkingHoursModel WorkingHours { get; set; }
        public bool IsOpen { get; set; }
        public IEnumerable<CarWashServiceModel> Services { get; set; } = new List<CarWashServiceModel>();

        public CarWashToClientFullModel ToModel(CarWashFullEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            Phone = entity.Phone;
            Location = entity.Location;
            CoordinateX = entity.CoordinateX;
            CoordinateY = entity.CoordinateY;
            Description = entity.Description;
            HasCafe = entity.HasCafe;
            HasRestZone = entity.HasRestZone;
            HasParking = entity.HasParking;
            HasWC = entity.HasWC;
            HasCardPayment = entity.HasCardPayment;
            WorkingHours = new CarWashWorkingHoursModel().ToModel(entity);
            IsOpen = CarWashWorkingHoursExtensions.IsCarWashOpen(entity);
            return this;
        }
    }

    public class CarWashToClientShortModel : IEntityToModelConvertible<CarWashFullEntity, CarWashToClientShortModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public bool IsOpen { get; set; }

        public CarWashToClientShortModel ToModel(CarWashFullEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            Name = entity.Name;
            Location = entity.Location;
            IsOpen = CarWashWorkingHoursExtensions.IsCarWashOpen(entity);
            return this;
        }
    }
}