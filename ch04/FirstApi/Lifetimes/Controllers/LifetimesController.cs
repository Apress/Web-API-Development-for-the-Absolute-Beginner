using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lifetimes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifetimesController : ControllerBase
    {
        private readonly ITransientOperation transient;
        private readonly ITransientOperation transient2;
        private readonly ISingletonOperation singleton;
        private readonly ISingletonOperation singleton2;
        private readonly IScopedOperation scoped;
        private readonly IScopedOperation scoped2;

        public LifetimesController(
            ITransientOperation transient,
            ITransientOperation transient2,
            ISingletonOperation singleton,
            ISingletonOperation singleton2,
            IScopedOperation scoped,
            IScopedOperation scoped2
            )
        {
            this.transient = transient;
            this.transient2 = transient2;

            this.singleton = singleton;
            this.singleton2 = singleton2;

            this.scoped = scoped;
            this.scoped2 = scoped2;
        }


        // GET api/<LifetimesController>/5
        [HttpGet("transient")]
        public string Get()
        {
            return $"LifetimeId-1:{transient.LifetimeId} \nLifetimeId-2:{transient2.LifetimeId}";
        }

        [HttpGet("singleton")]
        public string Singleton()
        {
            return $"LifetimeId-1:{singleton.LifetimeId} \nLifetimeId-2:{singleton2.LifetimeId}";
        }

        [HttpGet("scoped")]
        public string Scoped()
        {
            return $"LifetimeId-1:{ scoped.LifetimeId} \nLifetimeId-2:{scoped2.LifetimeId}";

        }


    }
}
