using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services, Action<IPipelinesBuilder> setupAction)
        {
            var pipelinesBuilder = new PipelinesBuilder();
            setupAction(pipelinesBuilder);
            var pipelines = pipelinesBuilder.Build();

            services.AddTransient<GetService>(c => c.GetService);
            services.AddTransient(c => pipelines);
            services.AddTransient<IMediator, Mediator>();

            return services;
        }
    }
}