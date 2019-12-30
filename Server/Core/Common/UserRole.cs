using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace VXDesign.Store.CarWashSystem.Server.Core.Common
{
    [Flags]
    public enum UserRole
    {
        [Description("Client")]
        Client = 1,

        [Description("Company")]
        Company = 2,

        Both = Client | Company
    }

    public static class UserRoleExtensions
    {
        public static string? GetUserRoleName(this UserRole userRole)
        {
            return userRole.GetType().GetMember(userRole.ToString())[0].GetCustomAttributes<DescriptionAttribute>(false).FirstOrDefault()?.Description;
        }

        public static UserRole? GetUserRole(string? userRoleName)
        {
            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
            return string.IsNullOrWhiteSpace(userRoleName) ? roles.FirstOrDefault(role => role.GetUserRoleName() == userRoleName) : (UserRole?) null;
        }
    }
}