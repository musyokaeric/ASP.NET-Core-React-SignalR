using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Activities;
using Reactivities.Domain;

namespace Reactivities.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<IActionResult> GetActivities([FromQuery] ActivityParams param) => HandlePagedResult(await Mediator.Send(new List.Query { Params = param}));

        [HttpGet("{id}")] //api/activities/{id}
        public async Task<IActionResult> GetActivity(Guid id) => HandleResult(await Mediator.Send(new Details.Query { Id = id }));

        [HttpPost] //api/activities
        public async Task<IActionResult> CreateActivity(Activity activity) => HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")] //api/activities/{id}
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")] //api/activities/{id}
        public async Task<IActionResult> DeleteActivity(Guid id) => HandleResult(await Mediator.Send(new Delete.Command { Id = id }));

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id) => HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
    }
}
