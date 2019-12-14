using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.ClientProfile
{
    public class CarModel : IEntityToModelConvertible<CarEntity, CarModel>, IModelToEntityConvertible<CarEntity>, IModelToEntityWithIdentifierConvertible<CarEntity>
    {
        public int Id { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        [StringLength(9)]
        public string? GovernmentPlate { get; set; }

        public CarModel ToModel(CarEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Id = entity.Id;
            ModelId = entity.ModelId;
            GovernmentPlate = entity.GovernmentPlate;
            return this;
        }

        public CarEntity ToEntity() => new CarEntity
        {
            ModelId = ModelId,
            GovernmentPlate = GovernmentPlate!
        };

        public CarEntity ToEntity(int id)
        {
            var entity = ToEntity();
            entity.Id = id;
            return entity;
        }
    }
}