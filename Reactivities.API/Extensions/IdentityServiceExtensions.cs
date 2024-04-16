using Reactivities.API.Services;
using Reactivities.Domain;
using Reactivities.Persistence;

namespace Reactivities.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication();

            // JWT Service
            services.AddScoped<TokenService>();

            return services;
        }
    }
}
