using Microsoft.AspNetCore.Http;
using Reactivities.Application.Interfaces;
using Reactivities.Application.Photos;

namespace Reactivities.Infrasctructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        public Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeletePhoto(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
