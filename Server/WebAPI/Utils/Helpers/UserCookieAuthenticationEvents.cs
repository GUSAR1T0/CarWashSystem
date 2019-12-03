using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Utils.Helpers
{
    public class UserCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ApplicationProperties properties;
        private readonly IUserAuthenticationService userAuthenticationService;

        public UserCookieAuthenticationEvents(ApplicationProperties properties, IUserAuthenticationService userAuthenticationService)
        {
            this.properties = properties;
            this.userAuthenticationService = userAuthenticationService;
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var user = context.Principal;
            if (properties.DatabaseConnectionString == null) throw new Exception(ExceptionMessage.DatabaseConnectionIsMissed);
            await Operation.MakeAction(properties.DatabaseConnectionString, async operation =>
            {
                var possibleId = user.Claims.FirstOrDefault(c => string.Equals(c.Type, AccountClaimName.UserId, StringComparison.InvariantCultureIgnoreCase))?.Value;
                if (possibleId != null && int.TryParse(possibleId, out var id) && !await userAuthenticationService.IsActivated(operation, id))
                {
                    context.RejectPrincipal();
                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            });
        }
    }
}