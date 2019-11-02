using System;
using System.Threading.Tasks;
using CarWashSystem.Server.WebAPI.Properties;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Operation;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly ApplicationProperties properties;

        private class ErrorResult
        {
            public string Source { get; }
            public string Target { get; }
            public string Message { get; }
            public string StackTrace { get; }

            public ErrorResult(Exception exception)
            {
                Source = exception.Source;
                Target = exception.TargetSite.Name;
                Message = exception.Message;
                StackTrace = exception.StackTrace;
            }
        }

        public ApiController(ApplicationProperties properties)
        {
            this.properties = properties;
        }

        protected async Task<ActionResult> Exec(Func<IOperation, Task> action)
        {
            try
            {
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
                return await Operation.MakeAction(properties.DatabaseConnectionString, action);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResult(e));
            }
        }
    }
}