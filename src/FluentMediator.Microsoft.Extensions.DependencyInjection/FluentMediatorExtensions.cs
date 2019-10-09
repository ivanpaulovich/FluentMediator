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
            services.AddSingleton<IMediator>(c => pipelinesManager.Build(c.GetService));
            
            return services;
        }
    }
}