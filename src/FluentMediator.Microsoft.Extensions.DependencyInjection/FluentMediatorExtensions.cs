using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    /// <summary>
    /// FluentMediatorExtensions
    /// </summary>
    public static class FluentMediatorExtensions
    {
        /// <summary>
        /// Adds the FluentMediator
        /// </summary>
        /// <param name="services">The ServiceCollection</param>
        /// <param name="setupAction">Builder</param>
        /// <returns>The changed ServiceCollection</returns>
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

        /// <summary>
        /// Adds the FluentMediator
        /// </summary>
        /// <param name="services">The ServiceCollection</param>
        /// <param name="setupAction">Builder</param>
        /// <returns>The changed ServiceCollection</returns>
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

        /// <summary>
        /// Adds the FluentMediator
        /// </summary>
        /// <param name="services">The ServiceCollection</param>
        /// <param name="setupAction">Builder</param>
        /// <returns>The changed ServiceCollection</returns>
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