﻿using Conference.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Conference.Domain
{
    public static class DataSeeder
    {

        public static void SeedData(ConferenceContext _context)
        {
            if (!_context.Speakers.Any())
            {
                _context.Speakers.AddRange(LoadSpeakers());
                _context.SaveChanges();
            }
        }

        private static List<Speaker> LoadSpeakers()
        {
            var jsonPath = @"D:\learning\MyGit\book\ch08\end\ConferenceApi\Conference.Domain\dummydata.json";
            using (StreamReader file = File.OpenText(jsonPath))
            {

                JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                var speakers = (List<Speaker>)serializer.Deserialize(file, typeof(List<Speaker>));
                return speakers;
            }
        }
    }
}
