using AutoMapper;
using Conference.Data;
using Conference.Service;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{
    [Route("api/speakers/{speakerId}/talks")]
    [ApiController]
    public class TalksForSpeakersController : ControllerBase
    {
        private readonly ITalksRepository talksRepository;
        private readonly ISpeakersService speakersService;
        private readonly IMapper mapper;

        public TalksForSpeakersController(ITalksRepository talksRepository,
            ISpeakersService speakersService, IMapper mapper
        )
        {
            this.talksRepository = talksRepository;
            this.speakersService = speakersService;
            this.mapper = mapper;
        }


        [HttpGet(Name = "GetTalksForSpeaker")]
        public ActionResult<IEnumerable<TalkModel>> GetTalksForSpeaker(int speakerId)
        {
            if (!speakersService.CheckIfExists(speakerId))
            {
                return NotFound();
            }
            var talksForSpeaker = talksRepository.GetTalksForSpeaker(speakerId);
            var talkModels = mapper.Map<IEnumerable<TalkModel>>(talksForSpeaker).ToList();
            return Ok(talkModels);
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
