using System.Threading;
using System.Threading.Tasks;
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

        [Fact]
        public async Task BuildAsyncPipeline()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<PingHandler, PingHandler>();
            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.AsyncPipeline<PingRequest>()
                .HandlerAsync<PingHandler>(async (handler, req) => await handler.MyMethodAsync(req));

            var ping = new PingRequest("Async Ping");
            await mediator.PublishAsync<PingRequest>(ping);
        }

        [Fact]
        public async Task BuildCancellableAsyncPipeline()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<PingHandler, PingHandler>();
            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.CancellableAsyncPipeline<PingRequest>()
                .HandlerAsync<PingHandler>(async (handler, req, ct) => await handler.MyMethodAsync(req, ct));

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");
            await mediator.PublishAsync<PingRequest>(ping, cts.Token);
        }
    }
}