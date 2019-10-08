using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services, Action<PipelinesManager> setupAction)
        {
            var pipelinesManager = new PipelinesManager();
            setupAction(pipelinesManager);

            services.AddTransient<GetService>(c => c.GetService);
            services.AddSingleton<IMediator, Mediator>();
            services.AddTransient<PipelinesManager>(c => pipelinesManager);
            
            return services;
        }
    }
}