using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Activities;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken token) => await Mediator.Send(new List.Query(), token);

        [HttpGet("{id}")] //api/activities/{id}
        public async Task<ActionResult<Activity>> GetActivity(Guid id) => await Mediator.Send(new Details.Query { Id = id });

        [HttpPost] //api/activities
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            await Mediator.Send(new Create.Command { Activity = activity });

            return Ok();
        }

        [HttpPut("{id}")] //api/activities/{id}
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            await Mediator.Send(new Edit.Command { Activity = activity });
            return Ok();
        }

        [HttpDelete("{id}")] //api/activities/{id}
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await Mediator.Send(new Delete.Command { Id = id });

            return Ok();
        }
    }
}
