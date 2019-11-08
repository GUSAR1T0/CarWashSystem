using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Web
{
    public class CompanySignInModel : IModelToEntityConvertible<CompanySignInEntity>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public CompanySignInEntity ToEntity() => new CompanySignInEntity
        {
            Email = Email,
            Password = Password
        };
    }

    public class CompanySignUpModel : IModelToEntityConvertible<CompanySignUpEntity>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public CompanySignUpEntity ToEntity() => new CompanySignUpEntity
        {
            Name = Name,
            Email = Email,
            Password = Password
        };
    }

    public class CompanyProfileModel : IEntityToModelConvertible<CompanyProfileEntity, CompanyProfileModel>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public CompanyProfileModel ToModel(CompanyProfileEntity? entity)
        {
            Id = entity?.Id;
            Name = entity?.Name;
            Email = entity?.Email;
            return this;
        }
    }
}