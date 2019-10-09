using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services, Action<MediatorBuilder> setupAction)
        {
            var pipelinesManager = new MediatorBuilder();
            setupAction(pipelinesManager);

            services.AddTransient<GetService>(c => c.GetService);
            services.AddSingleton<IMediator, Mediator>();
            services.AddTransient<MediatorBuilder>(c => pipelinesManager);
            
            return services;
        }
    }
}