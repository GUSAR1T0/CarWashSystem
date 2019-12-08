using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Validations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.ClientProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.ClientProfile
{
    public class ClientProfileModel : IEntityToModelConvertible<ClientProfileEntity, ClientProfileModel>, IModelToEntityWithIdentifierConvertible<ClientProfileEntity>
    {
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(32)]
        public string? Phone { get; set; }

        [AgeRestriction]
        public DateTime? Birthday { get; set; }

        public ClientProfileModel ToModel(ClientProfileEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            Phone = entity.Phone;
            Birthday = entity.Birthday;
            return this;
        }

        public ClientProfileEntity ToEntity(int id) => new ClientProfileEntity
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone,
            Birthday = Birthday
        };
    }
}