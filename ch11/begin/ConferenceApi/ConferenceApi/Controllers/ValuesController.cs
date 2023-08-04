using AutoMapper;
using Conference.Service;
using ConferenceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApi.Controllers
{

    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("get all test route");
        }
    }
}
