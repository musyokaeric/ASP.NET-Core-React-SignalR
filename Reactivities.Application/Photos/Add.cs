using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Core;
using Reactivities.Application.Interfaces;
using Reactivities.Domain;
using Reactivities.Persistence;

namespace Reactivities.Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly DataContext context;
            private readonly IPhotoAccessor photoAccessor;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                this.context = context;
                this.photoAccessor = photoAccessor;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername());

                if (user == null) return null;

                var photoUploadResult = await photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!user.Photos.Any(p => p.IsMain)) photo.IsMain = true;

                user.Photos.Add(photo);

                var result = await context.SaveChangesAsync() > 0;

                if (result) return Result<Photo>.Success(photo);

                return Result<Photo>.Failure("Problem adding photo");
            }
        }
    }
}
