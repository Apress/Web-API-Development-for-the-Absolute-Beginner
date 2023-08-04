using Asp.Versioning;
using AutoMapper;
using Conference.Api.Infrastructure.Attributes;
using Conference.Domain.Entities;
using Conference.Service;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ConferenceApi.Controllers
{

    //[Route("api/[controller]")]
    [ApiConventionType(typeof(MyApiConventions))]
    [ApiVersion("1.0")]
    [Route("api/speakers")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {

        private readonly ISpeakersService speakersService;
        private readonly IMapper mapper;

        public SpeakersController(ISpeakersService speakersService, IMapper mapper)
        {
            this.speakersService = speakersService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all speakers filtered by country
        /// </summary>
        /// <param name="country"></param>
        /// <returns>A list of SpeakerModel</returns>
        /// 

        [HttpGet]
        [CommaQueryString]
        //[ApiConventionMethod(typeof(DefaultApiConventions),
        //             nameof(DefaultApiConventions.Get))]
        public ActionResult<IEnumerable<SpeakerModel>> GetAll([FromQuery] List<string>? country)
        {
            var speakers = speakersService.GetAll();
            var speakerModels = mapper.Map<IEnumerable<SpeakerModel>>(speakers);
            return Ok(speakerModels);
        }

        /// <summary>
        /// Searches and returns a speaker by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A SpeakerModel</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Speakers/2
        ///
        /// </remarks>
        /// <example>
        /// 
        /// </example>
        /// <response code="200">Returns the Speaker</response>
        /// <response code="404">If the id is not found</response>
        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            var speakerToReturn = speakersService.Get(id);
            if (speakerToReturn == null)
            {
                return NotFound();
            }
            return Ok(speakerToReturn);
        }


        [HttpGet("{email:email}")]
        public IActionResult GetByEmail(string email)
        {
            Speaker speakerToReturn = null; //speakersService.Get(id);
            if (speakerToReturn == null)
            {
                return NotFound();
            }
            return Ok(speakerToReturn);
        }

        [HttpHead("{id}")]
        public IActionResult CheckIfExists(int id)
        {
            var speakerToReturn = speakersService.CheckIfExists(id);
            if (speakerToReturn == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SpeakerModel model)
        {
            var speakerFromDb = speakersService.Get(id);
            if (speakerFromDb == null)
            {
                return NotFound();
            }
            TryUpdateModelAsync(speakerFromDb);
            speakersService.Update(speakerFromDb);
            //update in the database
            return Ok(speakerFromDb);
        }
        /// <summary>
        /// Creates a new Speaker
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Speakers
        ///     {
        ///          "firstName": "string",
        ///          "lastName": "string",
        ///          "email": "user@example.com",
        ///          "website": "string",
        ///          "city": "string",
        ///          "country": "string"
        ///     }
        ///
        /// </remarks>
        [HttpPost]

        public IActionResult Post(SpeakerModel model)
        {
            if (!speakersService.IsEmailUnique(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email field should be unique");
                return Conflict(ModelState);
            }

            ///transform the entity from model to domain
            var speakerToAdd = mapper.Map<Speaker>(model);
            speakersService.Add(speakerToAdd);

            //return the item with the new assigned id
            return CreatedAtAction(nameof(Find), new { id = speakerToAdd.Id }, speakerToAdd);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var speakerFromDb = speakersService.Get(id);
            if (speakerFromDb == null)
            {
                return NotFound();
            }
            speakersService.Delete(speakerFromDb);
            return NoContent();
        }

    }
}
