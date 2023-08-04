using Asp.Versioning;
using AutoMapper;
using Conference.Domain.Entities;
using Conference.Service;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{
    [Route("api/speakers")]
    [ApiVersion("2.0")]
    [ApiController]
    public class SpeakersControllerV2 : ControllerBase
    {
        private readonly ISpeakersService speakersService;
        private readonly IMapper mapper;

        public SpeakersControllerV2(ISpeakersService speakersService, IMapper mapper)
        {
            this.speakersService = speakersService;
            this.mapper = mapper;
        }


        [HttpGet("{id}", Name ="GetSpeakerById2")]
        public ActionResult<SpeakerModelV2> GetSpeakerByIdV2(int id)
        {
            var speakerToReturn = speakersService.Get(id);
            if (speakerToReturn == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SpeakerModelV2>(speakerToReturn));
        }

    }
}
