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
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Common;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Authentication;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IUserAuthenticationService userAuthenticationService;

        public AccountController(ApplicationProperties properties, IUserAuthenticationService userAuthenticationService) : base(properties)
        {
            this.userAuthenticationService = userAuthenticationService;
        }

        #region Company

        /// <summary>
        /// Authenticates some user as company representative via cookies
        /// </summary>
        /// <param name="companySignInModel">Authorization company model</param>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("company/sign-in")]
        public async Task<ActionResult<CompanyProfileModel>> SignIn([FromBody] CompanySignInModel companySignInModel)
        {
            return await Exec(async operation =>
            {
                if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

                try
                {
                    if (!ModelState.IsValid) throw new Exception(ExceptionMessage.CompanySignInFailedDueToInvalidModel);
                    var profile = await userAuthenticationService.TrySignIn(operation, companySignInModel.ToEntity());
                    await LoginChallenge(profile, UserRole.Company);
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
        /// Registers new user as company representative via cookies
        /// </summary>
        /// <param name="companySignUpModel">Registration company model</param>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("company/sign-up")]
        public async Task<ActionResult<CompanyProfileModel>> SignUp([FromBody] CompanySignUpModel companySignUpModel)
        {
            return await Exec(async operation =>
            {
                if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

                try
                {
                    if (!ModelState.IsValid) throw new Exception(ExceptionMessage.CompanySignUpFailedDueToInvalidModel);
                    var profile = await userAuthenticationService.TrySignUp(operation, companySignUpModel.ToEntity());
                    await LoginChallenge(profile, UserRole.Company);
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
        [HttpPost("company/sign-out")]
        public async Task<ActionResult> SignOut() => await Exec(async _ => await LogoutChallenge());

        #endregion

        #region Client

        #endregion

        #region User

        /// <summary>
        /// Validates cookies
        /// </summary>
        /// <returns>User profile model if the process is successful</returns>
        [ProducesResponseType(typeof(UserProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("validate")]
        public async Task<ActionResult<UserProfileModel>> ValidateCookies()
        {
            return await Exec(async operation =>
            {
                var possibleId = User.Claims.FirstOrDefault(c => string.Equals(c.Type, AccountClaimName.UserId, StringComparison.InvariantCultureIgnoreCase))?.Value;
                var id = int.TryParse(possibleId, out var value) ? value : throw new Exception(ExceptionMessage.FailedToIdentifyUserId);
                var profile = await userAuthenticationService.GetUserProfile(operation, id);
                return new UserProfileModel().ToModel(profile);
            });
        }

        #endregion

        private async Task LoginChallenge(IUserProfileEntity? profile, string userRole)
        {
            if (profile != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(AccountClaimName.UserId, profile.Id.ToString()),
                    new Claim(AccountClaimName.UserEmail, profile.Email),
                    new Claim(AccountClaimName.UserRole, userRole)
                }, CookieAuthenticationDefaults.AuthenticationScheme)), new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true
                });
            }
            else throw new Exception(ExceptionMessage.CouldNotGetAccountProfile);
        }

        private async Task LogoutChallenge() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}