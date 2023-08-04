using AutoMapper;
using Conference.Domain.Entities;
using ConferenceApi.Models;

namespace ConferenceApi.Mappings
{
    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            CreateMap<Speaker, SpeakerModel>();
            CreateMap<SpeakerModel, Speaker>();
        }
    }
}
