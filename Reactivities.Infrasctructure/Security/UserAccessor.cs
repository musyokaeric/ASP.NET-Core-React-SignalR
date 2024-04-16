using Microsoft.AspNetCore.Http;
using Reactivities.Application.Interfaces;
using System.Security.Claims;

namespace Reactivities.Infrasctructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername() => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}
