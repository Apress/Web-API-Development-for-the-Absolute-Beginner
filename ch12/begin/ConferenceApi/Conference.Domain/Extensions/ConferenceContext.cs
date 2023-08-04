using Conference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Domain.Extensions
{


    public static class DbContextExtension
    {
        public static void EnsureSeeded(this ConferenceContext context)
        {
            DataSeeder.SeedData(context);
        }
    }
}
