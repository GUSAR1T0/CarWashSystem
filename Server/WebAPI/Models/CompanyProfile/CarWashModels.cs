using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CarWashFullModel : IEntityToModelConvertible<CarWashFullEntity, CarWashFullModel>, IModelToEntityConvertible<CarWashFullEntity>, IModelToEntityWithIdentifierConvertible<CarWashFullEntity>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [StringLength(512)]
        public string Location { get; set; } = "";

        [Required]
        public bool HasCafe { get; set; }

        [Required]
        public bool HasRestZone { get; set; }

        [Required]
        public bool HasParking { get; set; }

        [Required]
        public bool HasWC { get; set; }

        [Required]
        public bool HasCardPayment { get; set; }

        public CarWashFullModel ToModel(CarWashFullEntity? entity)
        {
            Name = entity?.Name;
            Email = entity?.Email;
            Phone = entity?.Phone;
            Location = entity?.Location;
            HasCafe = entity?.HasCafe ?? false;
            HasRestZone = entity?.HasRestZone ?? false;
            HasParking = entity?.HasParking ?? false;
            HasWC = entity?.HasWC ?? false;
            HasCardPayment = entity?.HasCardPayment ?? false;
            return this;
        }

        public CarWashFullEntity ToEntity() => new CarWashFullEntity
        {
            Name = Name,
            Email = Email,
            Phone = Phone,
            Location = Location,
            HasCafe = HasCafe,
            HasRestZone = HasRestZone,
            HasParking = HasParking,
            HasWC = HasWC,
            HasCardPayment = HasCardPayment
        };

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
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }
    }
}