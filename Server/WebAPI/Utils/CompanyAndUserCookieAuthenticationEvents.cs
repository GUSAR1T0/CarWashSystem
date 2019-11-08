using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Common;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Utils
{
    public class CompanyAndUserCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ApplicationProperties properties;
        private readonly ICompanyAuthenticationService companyAuthenticationService;

        public CompanyAndUserCookieAuthenticationEvents(ApplicationProperties properties, ICompanyAuthenticationService companyAuthenticationService)
        {
            this.properties = properties;
            this.companyAuthenticationService = companyAuthenticationService;
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var user = context.Principal;
            if (properties.DatabaseConnectionString == null) throw new Exception("Database connection string is not set");
            await Operation.MakeAction(properties.DatabaseConnectionString, async operation =>
            {
                var possibleCompanyId = user.Claims.FirstOrDefault(c => string.Equals(c.Type, WebClaimName.CompanyId, StringComparison.InvariantCultureIgnoreCase))?.Value;
                if (possibleCompanyId != null && int.TryParse(possibleCompanyId, out var companyId) && !await companyAuthenticationService.IsActive(operation, companyId))
                {
                    context.RejectPrincipal();
                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            });
        }
    }
}