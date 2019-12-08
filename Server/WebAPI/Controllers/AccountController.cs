using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Vkontakte;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.Authentication;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Extensions;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models.Authentication;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IConfiguration configuration;
        private readonly IUserAuthenticationService userAuthenticationService;

        public AccountController(ApplicationProperties properties, IConfiguration configuration, IUserAuthenticationService userAuthenticationService) : base(properties)
        {
            this.configuration = configuration;
            this.userAuthenticationService = userAuthenticationService;
        }

        #region Company

        /// <summary>
        /// Obtains company authentication profile
        /// </summary>
        /// <returns>Company profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("company")]
        public async Task<ActionResult<CompanyAuthenticationProfileModel>> GetCompanyProfile() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Company);
            var profile = await userAuthenticationService.GetCompanyProfile(operation, id);
            return new CompanyAuthenticationProfileModel
            {
                YandexMapsApiKey = configuration["Plugins:YandexMaps:ApiKey"],
                DadataApiKey = configuration["Plugins:DaData:ApiKey"]
            }.ToModel(profile);
        });

        /// <summary>
        /// Authenticates some user as company representative via cookies
        /// </summary>
        /// <param name="companySignInModel">Authorization company model</param>
        /// <returns>Company authentication profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("company/sign-in")]
        public async Task<ActionResult<CompanyAuthenticationProfileModel>> SignIn([FromBody] CompanySignInModel companySignInModel) => await Exec(async operation =>
        {
            if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

            try
            {
                if (!ModelState.IsValid) throw new Exception(ExceptionMessage.CompanySignInFailedDueToInvalidModel);
                var profile = await userAuthenticationService.TrySignIn(operation, companySignInModel.ToEntity());
                await LoginChallenge(profile, UserRole.Company);
                return new CompanyAuthenticationProfileModel
                {
                    YandexMapsApiKey = configuration["Plugins:YandexMaps:ApiKey"],
                    DadataApiKey = configuration["Plugins:DaData:ApiKey"]
                }.ToModel(profile);
            }
            catch
            {
                await LogoutChallenge();
                throw;
            }
        });

        /// <summary>
        /// Registers new user as company representative via cookies
        /// </summary>
        /// <param name="companySignUpModel">Registration company model</param>
        /// <returns>Company authentication profile model if the process is successful</returns>
        [ProducesResponseType(typeof(CompanyAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("company/sign-up")]
        public async Task<ActionResult<CompanyAuthenticationProfileModel>> SignUp([FromBody] CompanySignUpModel companySignUpModel) => await Exec(async operation =>
        {
            if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

            try
            {
                if (!ModelState.IsValid) throw new Exception(ExceptionMessage.CompanySignUpFailedDueToInvalidModel);
                var profile = await userAuthenticationService.TrySignUp(operation, companySignUpModel.ToEntity());
                await LoginChallenge(profile, UserRole.Company);
                return new CompanyAuthenticationProfileModel
                {
                    YandexMapsApiKey = configuration["Plugins:YandexMaps:ApiKey"],
                    DadataApiKey = configuration["Plugins:DaData:ApiKey"]
                }.ToModel(profile);
            }
            catch
            {
                await LogoutChallenge();
                throw;
            }
        });

        #endregion

        #region Client

        /// <summary>
        /// Obtains client authentication profile
        /// </summary>
        /// <returns>Client authentication profile model if the process is successful</returns>
        [ProducesResponseType(typeof(ClientAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("client")]
        public async Task<ActionResult<ClientAuthenticationProfileModel>> GetClientProfile() => await Exec(async operation =>
        {
            var id = VerifyUser(UserRole.Client);
            var profile = await userAuthenticationService.GetClientProfile(operation, id);
            return new ClientAuthenticationProfileModel().ToModel(profile);
        });

        /// <summary>
        /// Authenticates some user as client via cookies
        /// </summary>
        /// <param name="clientSignInModel">Authorization client model</param>
        /// <returns>Client authentication profile model if the process is successful</returns>
        [ProducesResponseType(typeof(ClientAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("client/sign-in")]
        public async Task<ActionResult<ClientAuthenticationProfileModel>> SignIn([FromBody] ClientSignInModel clientSignInModel) => await Exec(async operation =>
        {
            if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

            try
            {
                if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ClientSignInFailedDueToInvalidModel);
                var profile = await userAuthenticationService.TrySignIn(operation, clientSignInModel.ToEntity());
                await LoginChallenge(profile, UserRole.Client);
                return new ClientAuthenticationProfileModel().ToModel(profile);
            }
            catch
            {
                await LogoutChallenge();
                throw;
            }
        });

        /// <summary>
        /// Registers new user as client via cookies
        /// </summary>
        /// <param name="clientSignUpModel">Registration client model</param>
        /// <returns>Client authentication profile model if the process is successful</returns>
        [ProducesResponseType(typeof(ClientAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("client/sign-up")]
        public async Task<ActionResult<ClientAuthenticationProfileModel>> SignUp([FromBody] ClientSignUpModel clientSignUpModel) => await Exec(async operation =>
        {
            if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

            try
            {
                if (!ModelState.IsValid) throw new Exception(ExceptionMessage.ClientSignUpFailedDueToInvalidModel);
                var profile = await userAuthenticationService.TrySignUp(operation, clientSignUpModel.ToEntity());
                await LoginChallenge(profile, UserRole.Client);
                return new ClientAuthenticationProfileModel().ToModel(profile);
            }
            catch
            {
                await LogoutChallenge();
                throw;
            }
        });

        /// <summary>
        /// Initializes external authentication
        /// </summary>
        /// <param name="schema">Type of external authentication</param>
        /// <returns>Result of challenge</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpGet("client/sign-in/external/initialize")]
        public IActionResult InitializeExternalSignIn([FromQuery] ExternalUserAuthenticationSchema schema)
        {
            try
            {
                if (User.Identity.IsAuthenticated) throw new Exception(ExceptionMessage.UserHasAlreadyAuthenticated);

                string GetExternalAuthenticationScheme() => schema switch
                {
                    ExternalUserAuthenticationSchema.Google => GoogleDefaults.AuthenticationScheme,
                    ExternalUserAuthenticationSchema.Vk => VkontakteAuthenticationDefaults.AuthenticationScheme,
                    _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
                };

                return new ChallengeResult(GetExternalAuthenticationScheme(), new AuthenticationProperties
                {
                    RedirectUri = $"/api/account/client/sign-in/external/verify?schema={schema:D}"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResult(e));
            }
        }

        /// <summary>
        /// Verifies external authentication
        /// </summary>
        /// <param name="schema">Type of external authentication</param>
        /// <returns>Redirect link</returns>
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpGet("client/sign-in/external/verify")]
        public async Task<RedirectResult> VerifyExternalSignIn([FromQuery] ExternalUserAuthenticationSchema schema)
        {
            var token = Guid.Empty;
            await Exec(async operation =>
            {
                token = await userAuthenticationService.TryExternalSignIn(operation, new ExternalClientSignInEntity
                {
                    ExternalId = User.Claims.Get(ClaimTypes.NameIdentifier),
                    FirstName = User.Claims.Get(ClaimTypes.GivenName) ?? User.Claims.Get(ClaimTypes.Name) ?? "",
                    LastName = User.Claims.Get(ClaimTypes.Surname) ?? "",
                    Email = User.Claims.Get(ClaimTypes.Email),
                    Schema = schema
                });
            });
            return Redirect($"cws://sign-in?token={token}");
        }

        /// <summary>
        /// Completes external authentication
        /// </summary>
        /// <param name="token">Token for external authentication</param>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(typeof(ClientAuthenticationProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpGet("client/sign-in/external/complete")]
        public async Task<ActionResult<ClientAuthenticationProfileModel>> CompleteExternalSignIn([FromQuery] Guid token)
        {
            return await Exec(async operation =>
            {
                var profile = await userAuthenticationService.TryExternalSignIn(operation, token);
                await LoginChallenge(profile, UserRole.Client);
                return new ClientAuthenticationProfileModel().ToModel(profile);
            });
        }

        #endregion

        #region User

        /// <summary>
        /// Revokes cookies and logs out
        /// </summary>
        /// <returns>Nothing to return</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpDelete("sign-out")]
        public async Task<ActionResult> SignOut() => await Exec(async _ => await LogoutChallenge());

        #endregion

        private async Task LoginChallenge(IUserAuthenticationProfileEntity? profile, string userRole)
        {
            if (profile != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(AccountClaimName.UserId, profile.Id.ToString()),
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