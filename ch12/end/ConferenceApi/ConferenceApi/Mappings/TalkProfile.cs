using AutoMapper;
using Conference.Domain.Entities;
using ConferenceApi.Models;

namespace ConferenceApi.Mappings
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            CreateMap<Talk, TalkModel>();
        }
    }
}
