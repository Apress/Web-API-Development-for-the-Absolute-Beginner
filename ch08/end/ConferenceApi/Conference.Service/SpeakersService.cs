using Conference.Data;
using Conference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Service
{
    public interface ISpeakersService
    {
        Speaker Add(Speaker newSpeaker);
        IEnumerable<Speaker> GetAll();
        Speaker Get(int id);
        Speaker Update(Speaker speaker);
        bool Delete(Speaker speaker);
        bool CheckIfExists(int id);
        bool IsEmailUnique(string email);
    }

    public class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository speakersRepository;

        public SpeakersService(ISpeakersRepository speakersRepository)
        {
            this.speakersRepository = speakersRepository;
        }

        public Speaker Add(Speaker newSpeaker)
        {
            return speakersRepository.Add(newSpeaker);
        }

        public bool CheckIfExists(int id)
        {
            return speakersRepository.SpeakerExists(id);
        }

        public bool Delete(Speaker speaker)
        {
            return speakersRepository.Delete(speaker);
        }

        public Speaker Get(int id)
        {
           return speakersRepository.Get(id);
        }

        public IEnumerable<Speaker> GetAll()
        {
            var speakers = speakersRepository.GetAll();
            return speakers;
        }

        public bool IsEmailUnique(string email)
        {
            return speakersRepository.IsEmailUnique(email);
        }

        public Speaker Update(Speaker speaker)
        {
            return speakersRepository.Update(speaker);
        }
    }
}
