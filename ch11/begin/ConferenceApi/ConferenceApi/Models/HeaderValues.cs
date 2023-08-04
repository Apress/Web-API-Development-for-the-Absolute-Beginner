using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Models
{
    public class HeaderValues
    {
        [FromHeader]
        public string Accept { get; set; }

        [FromHeader(Name = "Culture")]
        public string Culture { get; set; }

        [FromHeader(Name = "Language")]
        public string Language { get; set; }
    }
}
