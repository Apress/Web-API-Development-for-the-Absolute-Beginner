using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers
{
    [Route("api/[area]/[controller]")]
    [Area("admin")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> GetMyResponse()
        {
            return "response from Get";
        }
    }
        //[HttpGet]
        //[Route("/myproducts")]
        //public ActionResult<string> Get()
        //{
        //    return "response from Get";
        //}


        [HttpPost]
        public ActionResult<string> Post()
        {
            return "response from POST";
        }

        //[HttpGet]
        //[Route("[action]")]
        //public ActionResult<string> GetMyResponse()
        //{
        //    return "response from Get";
        //}



        //[HttpGet("/products2/{id}", Name = "Products_List")]
        //public ActionResult<string> GetProduct(int id)
        //{
        //    return $"response from GET with {id} parameter"; ;
        //}
    }
}
