﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Activities;
using Reactivities.Application.Core;
using Reactivities.Application.Interfaces;
using Reactivities.Infrasctructure.Security;
using Reactivities.Persistence;

namespace Reactivities.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // MediatR Service
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
            // registers all services in the same assembly as List.Hander = Reactivities.Application.Activities namespace

            // Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();

            // Client: CORS policy
            services.AddCors(options =>
            {
                string clientUrl = "http://localhost:3000";
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(clientUrl);
                });
            });

            // HTTP Context Accessor
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();

            // Automapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}
