using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwesomeController : ControllerBase
    {

        //[HttpGet]
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Home/Index/{id?}")]
        //public IActionResult MyAction(int? id)
        //{
        //    return Ok("Awesome controller-get ");
        //} 

        [HttpGet]
        [Route("")]
        [Route("/Home")]
        [Route("/Home/Index")]
        [Route("/Home/Index/{id?}")]
        public IActionResult MyAction2(int? id)
        {
            return Ok("Awesome controller-get ");
        }

        [HttpGet]
        [Route("~/")]
        public IActionResult MyAction3(int? id)
        {
            return Ok("Awesome controller-get ");
        }
    }
}
