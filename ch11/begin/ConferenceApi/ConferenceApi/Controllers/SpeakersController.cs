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
    [Route("api/[controller]")]
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

        [HttpGet]
        [CommaQueryString]
        public ActionResult<IEnumerable<SpeakerModel>> GetAll([FromQuery] List<string>? country)
        {
            var speakers = speakersService.GetAll();
            var speakerModels = mapper.Map<IEnumerable<SpeakerModel>>(speakers);
            return Ok(speakerModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
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
            return CreatedAtAction(nameof(GetbyId), new { id = speakerToAdd.Id }, speakerToAdd);
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
