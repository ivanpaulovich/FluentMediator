using System;
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
        public void PipelineBuilder()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().Pipeline()
                .Call<IPingHandler>((handler, req) => handler.MyMethod(req))
                .Call<IPingHandler>((handler, req) => handler.MyLongMethod(req));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            mediator.Publish(ping);

            pingHandler.Verify(e => e.MyMethod(ping), Times.Once);
            pingHandler.Verify(e => e.MyLongMethod(ping), Times.Once);
        }

        [Fact]
        public async Task BuildAsyncPipeline()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().AsyncPipeline()
                .Call<IPingHandler>(async(handler, req) => await handler.MyMethodAsync(req))
                .Build();
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Async Ping");
            await mediator.PublishAsync(ping);

            pingHandler.Verify(e => e.MyMethodAsync(ping), Times.Once);
        }

        [Fact]
        public async Task BuildCancellableAsyncPipeline()
        {
            
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().CancellablePipeline()
                .Call<IPingHandler>(async(handler, req, ct) => await handler.MyMethodAsync(req, ct))
                .Build();
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");
            await mediator.PublishAsync(ping, cts.Token);

            pingHandler.Verify(e => e.MyMethodAsync(ping, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task BuildCancellableAsyncPipelineDirect()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().CancellablePipeline()
                .Call<IPingHandler>(async(handler, req, ct) => await handler.MyMethodAsync(req, ct))
                .Return<PingResponse, IPingHandler>(
                    async(handler, req, ct) => await handler.MyMethodAsync(req, ct)
                );
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var cts = new CancellationTokenSource();
            var ping = new PingRequest("Cancellable Async Ping");
            await mediator.PublishAsync(ping, cts.Token);

            pingHandler.Verify(e => e.MyMethodAsync(ping, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void SendPipelineBuilder()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>()
                .Pipeline()
                .Return<PingResponse, IPingHandler>(
                    (handler, req) => handler.MyMethod(req)
                );
            });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            var response = mediator.Send<PingResponse>(ping);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task BuildSendAsyncPipeline()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().AsyncPipeline()
                .Return<PingResponse, IPingHandler>(
                    (handler, req) => handler.MyMethodAsync(req)
                );
            });
            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            
            var ping = new PingRequest("Ping");
            var response = await mediator.SendAsync<PingResponse>(ping);

            Assert.NotNull(response);
        }

        [Fact]
        public void BuildSendAsyncPipeline_ThrowsException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().AsyncPipeline()
                .Return<PingResponse, IPingHandler>(
                    (handler, req) => handler.MyMethodAsync(req)
                );

                Exception ex = Record.Exception(() =>
                {
                    m.On<PingRequest>().AsyncPipeline()
                        .Return<PingResponse, IPingHandler>(
                            (handler, req) => handler.MyMethodAsync(req)
                        );
                });

                Assert.NotNull(ex);
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
        }
    }
}