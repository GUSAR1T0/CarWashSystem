using System;
using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CompanyProfileModel : IEntityToModelConvertible<CompanyProfileEntity, CompanyProfileModel>, IModelToEntityWithIdentifierConvertible<CompanyProfileEntity>
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(32)]
        public string? Phone { get; set; }

        public CompanyProfileModel ToModel(CompanyProfileEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Name = entity.Name;
            Email = entity.Email;
            Phone = entity.Phone;
            return this;
        }

        public CompanyProfileEntity ToEntity(int id) => new CompanyProfileEntity
        {
            Id = id,
            Name = Name!,
            Email = Email!,
            Phone = Phone
        };
    }
}