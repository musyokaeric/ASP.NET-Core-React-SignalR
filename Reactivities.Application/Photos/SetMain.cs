using MediatR;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Core;
using Reactivities.Application.Interfaces;
using Reactivities.Persistence;

namespace Reactivities.Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this.context = context;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Include(u => u.Photos)
                    .FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername());

                if (user == null) return null;

                var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);

                if (photo == null) return null;

                var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);

                if (currentMain != null) currentMain.IsMain = false;

                photo.IsMain = true;

                var result = await context.SaveChangesAsync() > 0;

                if (result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem setting main photo");
            }
        }
    }
}
