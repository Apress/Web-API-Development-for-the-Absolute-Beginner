using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public class Repository
        {
            public string FindById(int id)
            {
                return string.Empty;
            }
        }
        private Repository repository;
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var databaseItem = repository.FindById(id);
        //    if (databaseItem==null)
        //    {
        //        //return a Not Found status code
        //       return NotFound();
        //      // return new NotFoundResult();
        //    }
        //    return Ok(databaseItem);
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get1(int id)
        {
            
            var databaseItem = repository.FindById(id);
            if (databaseItem == null)
            {
                return NotFound();
            }
            return databaseItem;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
