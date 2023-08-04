using Conference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Data
{
    public interface ISpeakersRepository
    {
        Speaker Add(Speaker newSpeaker);
        IQueryable<Speaker> GetAll();
        Speaker Get(int id);
        Speaker Update(Speaker speaker);
        bool Delete(Speaker speaker);
        bool SpeakerExists(int id);
        bool IsEmailUnique(string email);
    }

    public class SpeakersRepository : ISpeakersRepository
    {
        private readonly ConferenceContext context;

        public SpeakersRepository(ConferenceContext context)
        {
            this.context = context;
        }

        public Speaker Add(Speaker newSpeaker)
        {
            var addedItem = context.Add(newSpeaker).Entity;
            context.SaveChanges();
            return addedItem;
        }

        public bool SpeakerExists(int id)
        {
            return context.Speakers.Find(id) != null;
        }

        public bool Delete(Speaker speaker)
        {
            var deleted = context.Speakers.Remove(speaker);
            context.SaveChanges();
            return deleted != null;
        }

        public Speaker Get(int id)
        {
            return context.Speakers.Find(id);
        }

        public IQueryable<Speaker> GetAll()
        {
            return context.Speakers.AsQueryable();
        }

        public Speaker Update(Speaker speaker)
        {
            var updatedEntity = context.Speakers.Update(speaker).Entity;
            context.SaveChanges();
            return updatedEntity;

        }

        public bool IsEmailUnique(string email)
        {
            return !context.Speakers.Any(s => s.Email == email);
        }
    }
}
