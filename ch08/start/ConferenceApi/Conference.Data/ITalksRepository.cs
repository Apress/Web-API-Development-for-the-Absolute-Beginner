using Conference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Data
{
 
    public interface ITalksRepository
    {
        Talk Add(Talk newTalk);
        IQueryable<Talk> GetAll();
        Talk Get(int id);
        Talk Update(Talk talk);
        bool Delete(Talk talk);
        bool TalkExists(int id);
    }
}
