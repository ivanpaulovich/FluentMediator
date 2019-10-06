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
                .Handler<PingHandler>((h, r) => h.MyHandler(r))
                .Handler<PingHandler>((h, r) => h.LongHandler(r));

            var ping = new PingRequest("Ping");
            mediator.Publish<PingRequest>(ping);
        }
    }
}