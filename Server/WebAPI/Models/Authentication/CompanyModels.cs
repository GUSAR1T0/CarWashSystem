using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Authentication
{
    public class CompanySignInModel : IModelToEntityConvertible<CompanySignInEntity>
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        public CompanySignInEntity ToEntity() => new CompanySignInEntity
        {
            Email = Email!,
            Password = Password!
        };
    }

    public class CompanySignUpModel : IModelToEntityConvertible<CompanySignUpEntity>
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        public CompanySignUpEntity ToEntity() => new CompanySignUpEntity
        {
            Name = Name!,
            Email = Email!,
            Password = Password!
        };
    }

    public class CompanyProfileModel : IEntityToModelConvertible<CompanyProfileEntity, CompanyProfileModel>
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }

        public CompanyProfileModel ToModel(CompanyProfileEntity? entity)
        {
            Id = entity?.Id;
            Email = entity?.Email;
            Name = entity?.Name;
            return this;
        }
    }
}