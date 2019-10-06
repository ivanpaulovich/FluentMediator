using System.Threading;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnitTests.PingPong;
using Xunit;

namespace UnitTests
{
    public class MediatorTests
    {
        [Fact]
        public void BuildPipeline()
        {
            var pingHandler = new Mock<IPingHandler>();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.Pipeline<PingRequest>()
                .Handler<IPingHandler>((handler, req) => handler.MyMethod(req))
                .Handler<IPingHandler>((handler, req) => handler.MyLongMethod(req));

            var ping = new PingRequest("Ping");
            mediator.Publish<PingRequest>(ping);

            pingHandler.Verify(e => e.MyMethod(ping), Times.Once);
            pingHandler.Verify(e => e.MyLongMethod(ping), Times.Once);

        }

        [Fact]
        public async Task BuildAsyncPipeline()
        {
            var pingHandler = new Mock<IPingHandler>();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPingHandler>(provider => pingHandler.Object);
            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.AsyncPipeline<PingRequest>()
                .HandlerAsync<IPingHandler>(async (handler, req) => await handler.MyMethodAsync(req));

            var ping = new PingRequest("Async Ping");
            await mediator.PublishAsync<PingRequest>(ping);

            pingHandler.Verify(e => e.MyMethodAsync(ping), Times.Once);

        }

        [Fact]
        public async Task BuildCancellableAsyncPipeline()
        {
            var pingHandler = new Mock<IPingHandler>();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPingHandler>(provider => pingHandler.Object);
            var provider = serviceCollection.BuildServiceProvider();

            var mediator = new Mediator((type) => provider.GetService(type));
            mediator.CancellablePipeline<PingRequest>()
                .HandlerAsync<IPingHandler>(async (handler, req, ct) => await handler.MyMethodAsync(req, ct));

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");
            await mediator.PublishAsync<PingRequest>(ping, cts.Token);

            pingHandler.Verify(e => e.MyMethodAsync(ping, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}