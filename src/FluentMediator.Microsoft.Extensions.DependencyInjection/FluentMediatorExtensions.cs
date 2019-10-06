using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services)
        {
            services.AddTransient<GetService>(c => c.GetService);
            services.AddSingleton<Mediator, Mediator>();

            return services;
        }
    }
}