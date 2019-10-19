using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services, Action<IPipelineProviderBuilder> setupAction)
        {
            var pipelineProviderBuilder = new PipelineProviderBuilder();
            setupAction(pipelineProviderBuilder);
            var pipelineProvider = pipelineProviderBuilder.Build();

            services.AddTransient<GetService>(c => c.GetService);
            services.AddTransient(c => pipelineProvider);
            services.AddTransient<IMediator, Mediator>();

            return services;
        }

        public static IServiceCollection AddSingletonFluentMediator(
            this IServiceCollection services,
            Action<IPipelineProviderBuilder> setupAction)
        {
            var pipelineProviderBuilder = new PipelineProviderBuilder();
            setupAction(pipelineProviderBuilder);
            var pipelineProvider = pipelineProviderBuilder.Build();

            services.AddSingleton(c => pipelineProvider);
            services.AddSingleton<GetService>(c => c.GetService);
            services.AddSingleton<IMediator, Mediator>();

            return services;;
        }

        public static IServiceCollection AddScopedFluentMediator(
            this IServiceCollection services,
            Action<IPipelineProviderBuilder> setupAction)
        {
            var pipelineProviderBuilder = new PipelineProviderBuilder();
            setupAction(pipelineProviderBuilder);
            var pipelineProvider = pipelineProviderBuilder.Build();

            services.AddScoped(c => pipelineProvider);
            services.AddScoped<GetService>(c => c.GetService);
            services.AddScoped<IMediator, Mediator>();

            return services;
        }
    }
}