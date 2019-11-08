using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Common;
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

        protected async Task<ActionResult> Exec(Func<IOperation, Task> action)
        {
            try
            {
                if (properties.DatabaseConnectionString == null) throw new Exception("Database connection string is not set");
                await Operation.MakeAction(properties.DatabaseConnectionString, action);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResult(e));
            }
        }

        protected async Task<ActionResult<T>> Exec<T>(Func<IOperation, Task<T>> action)
        {
            try
            {
                if (properties.DatabaseConnectionString == null) throw new Exception("Database connection string is not set");
                return await Operation.MakeAction(properties.DatabaseConnectionString, action);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResult(e));
            }
        }
    }
}