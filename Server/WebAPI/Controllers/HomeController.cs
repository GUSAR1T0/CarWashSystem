using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Loads the Web application
        /// </summary>
        /// <returns>Prepared content of the application</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }

        /// <summary>
        /// Sends "bad request" response
        /// </summary>
        /// <returns>Error with message</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("error")]
        public IActionResult Error()
        {
            return BadRequest("Error page");
        }

        [HttpGet("value")]
        public string Method() => "{\"value\": 5}";
    }
}