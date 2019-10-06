using FluentMediator;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.PingPong;
using Xunit;

namespace UnitTests
{
    public class MediatorTests
    {
        [Fact]
        public void BuildPipeline()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<PingHandler, PingHandler>();
            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.Pipeline<PingRequest>()
                .Handler<PingHandler>((handler, req) => handler.MyMethod(req))
                .Handler<PingHandler>((handler, req) => handler.MyLongMethod(req));

            var ping = new PingRequest("Ping");
            mediator.Publish<PingRequest>(ping);
        }
    }
}