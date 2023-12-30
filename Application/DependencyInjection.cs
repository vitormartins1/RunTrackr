﻿using Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
            });

            return services;
        }
    }
}
