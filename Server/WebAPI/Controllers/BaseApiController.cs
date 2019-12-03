using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.Core.Operation;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Extensions;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Properties;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        private readonly ApplicationProperties properties;

        protected BaseApiController(ApplicationProperties properties)
        {
            this.properties = properties;
        }

        protected ActionResult Exec(Action<IOperation> action)
        {
            var result = Exec(async operation =>
            {
                await Task.Run(() => action(operation));
                return true;
            }).Result;
            return result.Value ? Ok() : result.Result;
        }

        protected ActionResult<T> Exec<T>(Func<IOperation, T> action)
        {
            return Exec(async operation => await Task.Run(() => action(operation))).Result;
        }

        protected async Task<ActionResult> Exec(Func<IOperation, Task> action)
        {
            var result = await Exec(async operation =>
            {
                await action(operation);
                return true;
            });
            return result.Value ? Ok() : result.Result;
        }

        protected async Task<ActionResult<T>> Exec<T>(Func<IOperation, Task<T>> action)
        {
            try
            {
                if (properties.DatabaseConnectionString == null) throw new Exception(ExceptionMessage.DatabaseConnectionIsMissed);
                return await Operation.MakeAction(properties.DatabaseConnectionString, action);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResult(e));
            }
        }

        protected int VerifyUser(string requiredUserRole)
        {
            var userRole = User.Claims.Get(AccountClaimName.UserRole);
            if (userRole != requiredUserRole) throw new Exception(ExceptionMessage.RoleIsNotSuitable(requiredUserRole));

            var possibleId = User.Claims.Get(AccountClaimName.UserId);
            var id = int.TryParse(possibleId, out var value) ? value : throw new Exception(ExceptionMessage.FailedToIdentifyUserId);

            return id;
        }
    }
}