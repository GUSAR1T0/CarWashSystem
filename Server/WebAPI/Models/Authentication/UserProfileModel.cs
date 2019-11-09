using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Authentication
{
    public class UserProfileModel : IEntityToModelConvertible<UserProfileEntity, UserProfileModel>
    {
        public int? Id { get; set; }
        public string? Email { get; set; }

        public UserProfileModel ToModel(UserProfileEntity? entity)
        {
            Id = entity?.Id;
            Email = entity?.Email;
            return this;
        }
    }
}