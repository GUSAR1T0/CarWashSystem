using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CarWashServicePriceModel : IEntityToModelConvertible<CarWashServicePriceEntity, CarWashServicePriceModel>, IModelToEntityConvertible<CarWashServicePriceEntity>,
        IModelToEntityWithIdentifierConvertible<CarWashServicePriceEntity>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; } = "";

        [StringLength(1024)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsAvailable { get; set; }

        public CarWashServicePriceModel ToModel(CarWashServicePriceEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            ServiceName = entity.ServiceName;
            Description = entity.Description;
            Price = entity.Price;
            Duration = entity.Duration;
            IsAvailable = entity.IsAvailable;
            return this;
        }

        public CarWashServicePriceEntity ToEntity() => new CarWashServicePriceEntity
        {
            ServiceName = ServiceName,
            Description = Description,
            Price = Price,
            Duration = Duration,
            IsAvailable = IsAvailable
        };

        public CarWashServicePriceEntity ToEntity(int id)
        {
            var entity = ToEntity();
            entity.Id = id;
            return entity;
        }
    }

    public class CarWashServicePriceShortModel : IEntityToModelConvertible<CarWashServicePriceShortEntity, CarWashServicePriceShortModel>
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = "";

        public CarWashServicePriceShortModel ToModel(CarWashServicePriceShortEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            ServiceName = entity.ServiceName;
            return this;
        }
    }
}