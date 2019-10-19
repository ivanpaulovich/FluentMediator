using System.Threading;
using System.Threading.Tasks;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnitTests.PingPong;
using Xunit;

namespace UnitTests
{
    public sealed class PublishingRequestsTests
    {
        [Fact]
        public void Publish_Calls_Pipeline_Handlers()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(builder =>
            {
                builder.On<PingRequest>().Pipeline()
                    .Call<IPingHandler>((handler, req) => handler.MyCustomFooMethod(req))
                    .Call<IPingHandler>((handler, req) => handler.MyCustomBarMethod(req));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");

            //
            // Act
            //
            mediator.Publish(ping);

            pingHandler.Verify(e => e.MyCustomFooMethod(ping), Times.Once);
            pingHandler.Verify(e => e.MyCustomBarMethod(ping), Times.Once);
        }

        [Fact]
        public async Task PublishAsync_Calls_AsyncPipeline_Handlers()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(builder =>
            {
                builder.On<PingRequest>().PipelineAsync()
                    .Call<IPingHandler>(async (handler, req) => await handler.MyCustomFooBarAsync(req))
                    .Build();
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Async Ping");

            //
            // Act
            //
            await mediator.PublishAsync(ping);

            pingHandler.Verify(e => e.MyCustomFooBarAsync(ping), Times.Once);
        }

        [Fact]
        public async Task PublishAsync_Calls_CancellablePipeline_Handlers()
        {

            var services = new ServiceCollection();
            services.AddFluentMediator(builder =>
            {
                builder.On<PingRequest>().CancellablePipelineAsync()
                    .Call<IPingHandler>(async (handler, req, ct) => await handler.MyCancellableForAsync(req, ct))
                    .Build();
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");

            //
            // Act
            //
            await mediator.PublishAsync(ping, cts.Token);

            pingHandler.Verify(e => e.MyCancellableForAsync(ping, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task PublishAsync_Calls_CancellablePipeline_Handlers2()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(builder =>
            {
                builder.On<PingRequest>().CancellablePipelineAsync()
                    .Call<IPingHandler>(async (handler, req, ct) => await handler.MyCancellableForAsync(req, ct))
                    .Return<PingResponse, IPingHandler>(
                        async (handler, req, ct) => await handler.MyCancellableForAsync(req, ct)
                    );
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");

            //
            // Act
            //
            await mediator.PublishAsync(ping, cts.Token);

            pingHandler.Verify(e => e.MyCancellableForAsync(ping, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}