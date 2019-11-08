using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Common;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Web;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers.Web
{
    [Route("api/web/account")]
    public class CompanyAccountController : BaseApiController
    {
        private readonly ICompanyAuthenticationService companyAuthenticationService;

        public CompanyAccountController(ApplicationProperties properties, ICompanyAuthenticationService companyAuthenticationService) : base(properties)
        {
            this.companyAuthenticationService = companyAuthenticationService;
        }

        /// <summary>
        /// Authenticates some user as company representative by cookies
        /// </summary>
        /// <param name="companySignInModel">Authorization company model</param>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<CompanyProfileModel>> SignIn([FromBody] CompanySignInModel companySignInModel)
        {
            return await Exec(async operation =>
            {
                try
                {
                    var profile = await companyAuthenticationService.TrySignIn(operation, companySignInModel.ToEntity());
                    await LoginChallenge(profile);
                    return new CompanyProfileModel().ToModel(profile);
                }
                catch
                {
                    await LogoutChallenge();
                    throw;
                }
            });
        }

        /// <summary>
        /// Registers new user as company representative by cookies
        /// </summary>
        /// <param name="companySignUpModel">Registration company model</param>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult<CompanyProfileModel>> SignUp([FromBody] CompanySignUpModel companySignUpModel)
        {
            return await Exec(async operation =>
            {
                try
                {
                    var profile = await companyAuthenticationService.TrySignUp(operation, companySignUpModel.ToEntity());
                    await LoginChallenge(profile);
                    return new CompanyProfileModel().ToModel(profile);
                }
                catch
                {
                    await LogoutChallenge();
                    throw;
                }
            });
        }

        /// <summary>
        /// Revokes cookies and logs out
        /// </summary>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("sign-out")]
        public async Task<ActionResult> SignOut() => await Exec(async _ => await LogoutChallenge());

        /// <summary>
        /// Validates cookies
        /// </summary>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("validate")]
        public async Task<ActionResult<CompanyProfileModel>> ValidateCookies()
        {
            return await Exec(async operation =>
            {
                var possibleId = User.Claims.FirstOrDefault(c => string.Equals(c.Type, WebClaimName.CompanyId, StringComparison.InvariantCultureIgnoreCase))?.Value;
                var id = int.TryParse(possibleId, out var value) ? value : throw new Exception("Failed to identify company ID");
                var profile = await companyAuthenticationService.GetProfile(operation, id);
                return new CompanyProfileModel().ToModel(profile);
            });
        }

        private async Task LoginChallenge(CompanyProfileEntity? profile)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(WebClaimName.CompanyId, profile?.Id.ToString()),
                new Claim(WebClaimName.CompanyName, profile?.Name),
                new Claim(WebClaimName.CompanyEmail, profile?.Email)
            }, CookieAuthenticationDefaults.AuthenticationScheme)), new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            });
        }

        private async Task LogoutChallenge()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}