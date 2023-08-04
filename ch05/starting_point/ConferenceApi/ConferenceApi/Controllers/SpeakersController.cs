using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private List<SpeakerModel> SpeakersList;
        public SpeakersController()
        {
            SpeakersList = new List<SpeakerModel>() {
                new SpeakerModel() {
                    Email="speaker1@mail.com",
                    FirstName="FirstName1",
                    LastName="LastName1",
                    Id=1
                },
                new SpeakerModel() {
                    Email="speaker2@mail.com",
                    FirstName="FirstName2",
                    LastName="LastName2",
                    Id=2
                },
             };
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(SpeakersList);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var speakerToReturn = SpeakersList.FirstOrDefault(x => x.Id == id);
            if (speakerToReturn == null)
            {
                return NotFound();
            }
            return Ok(speakerToReturn);
        }

        [HttpHead("{id}")]
        public IActionResult CheckIfExists(int id)
        {
            var speakerToReturn = SpeakersList.FirstOrDefault(x => x.Id == id);
            if (speakerToReturn == null)
            {
                return NotFound();
            }
            return Ok(speakerToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SpeakerModel model)
        {
            var speakerFromDb = SpeakersList.FirstOrDefault(x => x.Id == id);
            if (speakerFromDb == null)
            {
                return NotFound();
            }
            TryUpdateModelAsync(speakerFromDb);
            //update in the database
            return Ok(speakerFromDb);
        }

        [HttpPost]
        public IActionResult Post(SpeakerModel model)
        {
            //not necessary now
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            //if (SpeakersList.Any(x=>x.Email==model.Email))
            //{
            //    return Conflict("Email field should be unique");
            //}

            if (SpeakersList.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email field should be unique");
                return Conflict(ModelState);
            }
            //assign an Id
            model.Id = SpeakersList.Max(x => x.Id + 1);
            //add  in the 'db'
            SpeakersList.Add(model);
            //return the item with the new assigned id
            return CreatedAtAction(nameof(GetbyId), new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var speakerFromDb = SpeakersList.FirstOrDefault(x => x.Id == id);
            if (speakerFromDb == null)
            {
                return NotFound();
            }
            SpeakersList.Remove(speakerFromDb);
            return NoContent();
        }

    }
}
