using Asp.Versioning;
using AutoMapper;
using Conference.Data;
using Conference.Service;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/speakers/{speakerId}/talks")]
    [ApiController]
    public class TalksForSpeakersControllerV2 : ControllerBase
    {
        private readonly ITalksRepository talksRepository;
        private readonly ISpeakersService speakersService;
        private readonly IMapper mapper;

        public TalksForSpeakersControllerV2(ITalksRepository talksRepository,
            ISpeakersService speakersService, IMapper mapper
        )
        {
            this.talksRepository = talksRepository;
            this.speakersService = speakersService;
            this.mapper = mapper;
        }


        [HttpGet("{talkId}", Name = "GetTalkForSpeaker")]
        public ActionResult<TalkModel> GetTalksForSpeaker(int speakerId, int talkId)
        {
            if (!speakersService.CheckIfExists(speakerId))
            {
                return NotFound();
            }

            var talkForSpeaker = talksRepository.GetTalk(speakerId, talkId);

            if (talkForSpeaker == null)
            {
                return NotFound();
            }
            var talk = talksRepository.GetTalksForSpeaker(speakerId);
            var talkModel = mapper.Map<IEnumerable<TalkModel>>(talk);
            return Ok(talkModel);
        }
    }
}
