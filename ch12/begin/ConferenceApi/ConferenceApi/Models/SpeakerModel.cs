using ConferenceApi.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConferenceApi.Models
{
    [ModelBinder(BinderType = typeof(SpeakerEntityBinder))]
    public class SpeakerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
