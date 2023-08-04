using System.ComponentModel.DataAnnotations;

namespace ConferenceApi.Models
{
    public class SpeakerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
