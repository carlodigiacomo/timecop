using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using CommonLibrary.Infrastructure;
using CommonLibrary.User.Actors;
using CommonLibrary.User.Commands;
using CommonLibrary.User.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TimecopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IActorFactory _actorFactory;

        public ValuesController(IActorFactory actorFactory, ILogger<ValuesController> logger)
        {
            _actorFactory = actorFactory;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            _logger.LogInformation("start call to user actor");
            var userService = _actorFactory.FindTopLevelActor<UserService>();
            var command = new GetUserDetail(1);
            var result = await userService.Ask<UserDetailResult>(command, TimeSpan.FromSeconds(30));
            //var result = await _userManagerActor.Ask<UserDetailResult>(command, TimeSpan.FromSeconds(30));
            if (result.HasError) throw result.Exception;

            _logger.LogInformation("end call to user actor");
            return new ActionResult<string>($"{result.FirstName} {result.LastName}");
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
