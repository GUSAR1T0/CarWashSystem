using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CarWashServiceModel : IEntityToModelConvertible<CarWashServiceEntity, CarWashServiceModel>, IModelToEntityConvertible<CarWashServiceEntity>,
        IModelToEntityWithIdentifierConvertible<CarWashServiceEntity>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? ServiceName { get; set; }

        [StringLength(1024)]
        public string? Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public string? Duration { get; set; }

        public bool? IsAvailable { get; set; }

        public CarWashServiceModel ToModel(CarWashServiceEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            ServiceName = entity.ServiceName;
            Description = entity.Description;
            Price = entity.Price;
            Duration = entity.Duration.ToString(@"hh\:mm");
            IsAvailable = entity.IsAvailable;
            return this;
        }

        public CarWashServiceEntity ToEntity() => new CarWashServiceEntity
        {
            ServiceName = ServiceName!,
            Description = Description,
            Price = Price ?? new decimal(),
            Duration = TimeSpan.TryParse(Duration!, out var duration) ? duration : throw new Exception(ExceptionMessage.TimeSpanIsInvalid),
            IsAvailable = IsAvailable ?? true
        };

        public CarWashServiceEntity ToEntity(int id)
        {
            var entity = ToEntity();
            entity.Id = id;
            return entity;
        }
    }

    public class CarWashServiceShortModel : IEntityToModelConvertible<CarWashServiceShortEntity, CarWashServiceShortModel>
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = "";

        public CarWashServiceShortModel ToModel(CarWashServiceShortEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            ServiceName = entity.ServiceName;
            return this;
        }
    }
}