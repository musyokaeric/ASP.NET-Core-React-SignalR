using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reactivities.Domain;
using Reactivities.Persistence;

namespace Reactivities.Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext context;
            private readonly ILogger<List> logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Understanding cancellation tokens:
                // ==================================
                // Scenario: Suppose we had a logic that takes a long time to perform an action eg. getting a list of activities.
                // A user may think that the application is broken (especially when they see a loading spinner and nothing changes
                // for a considerable amout of time). Some users wait, others refresh/close the browser. But in the background,
                // the request might still be ongoing.
                // These cancellation tokens allow us to actually cancel the request if it's no longer needed.

                try
                {
                    for (var i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        logger.LogInformation($"Task '{i}' has completed");
                    }
                }
                catch (Exception)
                {
                    logger.LogInformation("Task was cancelled");
                }


                return await context.Activities.ToListAsync();
            }
        }
    }
}
