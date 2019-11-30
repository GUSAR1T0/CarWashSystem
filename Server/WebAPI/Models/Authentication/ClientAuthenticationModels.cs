using System.ComponentModel.DataAnnotations;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Authentication
{
    public class ClientSignInModel : IModelToEntityConvertible<ClientSignInEntity>
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        public ClientSignInEntity ToEntity() => new ClientSignInEntity
        {
            Email = Email!,
            Password = Password!
        };
    }
    
    public class ClientSignUpModel : IModelToEntityConvertible<ClientSignUpEntity>
    {
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        public ClientSignUpEntity ToEntity() => new ClientSignUpEntity
        {
            FirstName = FirstName!,
            LastName = LastName!,
            Email = Email!,
            Password = Password!
        };
    }
    
    public class ClientAuthenticationProfileModel : IEntityToModelConvertible<ClientAuthenticationProfileEntity, ClientAuthenticationProfileModel>
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ClientAuthenticationProfileModel ToModel(ClientAuthenticationProfileEntity? entity)
        {
            Id = entity?.Id;
            FirstName = entity?.FirstName;
            LastName = entity?.LastName;
            return this;
        }
    }
}