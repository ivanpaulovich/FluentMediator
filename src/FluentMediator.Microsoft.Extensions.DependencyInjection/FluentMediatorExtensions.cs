using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMediator
{
    public static class FluentMediatorExtensions
    {
        public static IServiceCollection AddFluentMediator(this IServiceCollection services, Action<IMediatorBuilder> setupAction)
        {
            var mediatorBuilder = new MediatorBuilder();
            setupAction(mediatorBuilder);
            services.AddSingleton<IMediator>(c => mediatorBuilder.Build(c.GetService));

            return services;
        }
    }
}