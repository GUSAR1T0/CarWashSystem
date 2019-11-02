using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashSystem.Server.WebAPI.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VXDesign.Store.CarWashSystem.Server.Services.Interfaces;
using VXDesign.Store.CarWashSystem.Server.WebAPI.Models;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : ApiController
    {
        private readonly IExampleService service;

        public ExampleController(ApplicationProperties properties, IExampleService service) : base(properties)
        {
            this.service = service;
        }

        [Obsolete("It's just an example")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("1")]
        public async Task<ActionResult<IEnumerable<ExampleEntityModel>>> GetExample()
        {
            return await Exec(async operation => (await service.GetExample1(operation)).Select(ExampleEntityModelExtensions.ToModel));
        }
    }
}