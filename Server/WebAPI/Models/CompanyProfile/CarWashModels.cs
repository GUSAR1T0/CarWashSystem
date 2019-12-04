using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CarWashFullModel : IEntityToModelConvertible<CarWashFullEntity, CarWashFullModel>, IModelToEntityConvertible<CarWashFullEntity>,
        IModelToEntityWithIdentifierConvertible<CarWashFullEntity>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(32)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(512)]
        public string Location { get; set; } = "";

        [Required]
        public decimal CoordinateX { get; set; }

        [Required]
        public decimal CoordinateY { get; set; }

        [StringLength(1024)]
        public string? Description { get; set; }

        public bool? HasCafe { get; set; }

        public bool? HasRestZone { get; set; }

        public bool? HasParking { get; set; }

        public bool? HasWC { get; set; }

        public bool? HasCardPayment { get; set; }

        public CarWashWorkingHoursModel? WorkingHours { get; set; }

        public CarWashFullModel ToModel(CarWashFullEntity? entity)
        {
            Id = entity?.Id ?? 0;
            Name = entity?.Name ?? "";
            Email = entity?.Email;
            Phone = entity?.Phone;
            Location = entity?.Location ?? "";
            CoordinateX = entity?.CoordinateX ?? new decimal();
            CoordinateY = entity?.CoordinateY ?? new decimal();
            Description = entity?.Description;
            HasCafe = entity?.HasCafe ?? false;
            HasRestZone = entity?.HasRestZone ?? false;
            HasParking = entity?.HasParking ?? false;
            HasWC = entity?.HasWC ?? false;
            HasCardPayment = entity?.HasCardPayment ?? false;
            WorkingHours = new CarWashWorkingHoursModel().ToModel(entity);
            return this;
        }

        public CarWashFullEntity ToEntity()
        {
            (TimeSpan? startTime, TimeSpan? stopTime)? mondayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? tuesdayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? wednesdayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? thursdayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? fridayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? saturdayHours = null;
            (TimeSpan? startTime, TimeSpan? stopTime)? sundayHours = null;
            var monday = WorkingHours?.Monday?.TryToGet(out mondayHours);
            var tuesday = WorkingHours?.Tuesday?.TryToGet(out tuesdayHours);
            var wednesday = WorkingHours?.Wednesday?.TryToGet(out wednesdayHours);
            var thursday = WorkingHours?.Thursday?.TryToGet(out thursdayHours);
            var friday = WorkingHours?.Friday?.TryToGet(out fridayHours);
            var saturday = WorkingHours?.Saturday?.TryToGet(out saturdayHours);
            var sunday = WorkingHours?.Sunday?.TryToGet(out sundayHours);
            return new CarWashFullEntity
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Location = Location,
                CoordinateX = CoordinateX,
                CoordinateY = CoordinateY,
                Description = Description,
                HasCafe = HasCafe ?? false,
                HasRestZone = HasRestZone ?? false,
                HasParking = HasParking ?? false,
                HasWC = HasWC ?? false,
                HasCardPayment = HasCardPayment ?? false,
                MondayStartTime = monday == true ? mondayHours?.startTime : null,
                MondayStopTime = monday == true  ? mondayHours?.stopTime : null,
                TuesdayStartTime = tuesday == true  ? tuesdayHours?.startTime : null,
                TuesdayStopTime = tuesday == true  ? tuesdayHours?.stopTime : null,
                WednesdayStartTime = wednesday == true  ? wednesdayHours?.startTime : null,
                WednesdayStopTime = wednesday == true  ? wednesdayHours?.stopTime : null,
                ThursdayStartTime = thursday == true  ? thursdayHours?.startTime : null,
                ThursdayStopTime = thursday == true  ? thursdayHours?.stopTime : null,
                FridayStartTime = friday == true  ? fridayHours?.startTime : null,
                FridayStopTime = friday == true  ? fridayHours?.stopTime : null,
                SaturdayStartTime = saturday == true  ? saturdayHours?.startTime : null,
                SaturdayStopTime = saturday == true  ? saturdayHours?.stopTime : null,
                SundayStartTime = sunday == true  ? sundayHours?.startTime : null,
                SundayStopTime = sunday == true  ? sundayHours?.stopTime : null
            };
        }

        public CarWashFullEntity ToEntity(int id)
        {
            var entity = ToEntity();
            entity.Id = id;
            return entity;
        }
    }

    public class CarWashShortModel : IEntityToModelConvertible<CarWashShortEntity, CarWashShortModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public CarWashShortModel ToModel(CarWashShortEntity? entity)
        {
            Id = entity?.Id ?? 0;
            Name = entity?.Name ?? "";
            return this;
        }
    }
}