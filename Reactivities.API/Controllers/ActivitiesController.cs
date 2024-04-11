using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Activities;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities() => HandleResult(await Mediator.Send(new List.Query()));

        [HttpGet("{id}")] //api/activities/{id}
        public async Task<IActionResult> GetActivity(Guid id) => HandleResult(await Mediator.Send(new Details.Query { Id = id }));

        [HttpPost] //api/activities
        public async Task<IActionResult> CreateActivity(Activity activity) => HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));

        [HttpPut("{id}")] //api/activities/{id}
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
        }

        [HttpDelete("{id}")] //api/activities/{id}
        public async Task<IActionResult> DeleteActivity(Guid id) => HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
