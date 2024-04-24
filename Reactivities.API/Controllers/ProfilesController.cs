using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Profiles;

namespace Reactivities.API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username) => HandleResult(await Mediator.Send(new Details.Query { Username = username }));

        [HttpPut]
        public async Task<IActionResult> Edit(Edit.Command command) => HandleResult(await Mediator.Send(command));

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username, string predicate) => HandleResult(await Mediator.Send(new ListActivities.Query { Username = username, Predicate = predicate }));
    }
}
