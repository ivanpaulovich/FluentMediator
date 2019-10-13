using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMediator;
using FluentMediator.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnitTests.PingPong;
using Xunit;

namespace UnitTests
{
    public class SendTests
    {
        [Fact]
        public void Send_FailFast_WhenMisconfiguredPipeline()
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
            
            var actualEx = Record.Exception(
                () => mediator.Send<PingResponse>(ping)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx);

            pingHandler.Verify(e => e.MyMethod(ping), Times.Never);
            pingHandler.Verify(e => e.MyLongMethod(ping), Times.Never);
        }

        [Fact]
        public void SendAsync_FailFast_WhenMisconfiguredPipeline()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().AsyncPipeline()
                .Call<IPingHandler>((handler, req) => handler.MyMethodAsync(req))
                .Call<IPingHandler>((handler, req) => handler.MyMethodAsync(req));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            
            var actualEx = Record.ExceptionAsync(
                async () => await mediator.SendAsync<PingResponse>(ping)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx.Result);

            pingHandler.Verify(e => e.MyMethod(ping), Times.Never);
            pingHandler.Verify(e => e.MyLongMethod(ping), Times.Never);
        }

        [Fact]
        public void SendAsyncCancellable_FailFast_WhenMisconfiguredPipeline()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
                m.On<PingRequest>().CancellablePipeline()
                .Call<IPingHandler>((handler, req, ct) => handler.MyMethodAsync(req, ct))
                .Call<IPingHandler>((handler, req, ct) => handler.MyMethodAsync(req, ct));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            var cts = new CancellationTokenSource();

            var ping = new PingRequest("Ping");
            
            var actualEx = Record.ExceptionAsync(
                async () => await mediator.SendAsync<PingResponse>(ping, cts.Token)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx.Result);

            pingHandler.Verify(e => e.MyMethod(ping), Times.Never);
            pingHandler.Verify(e => e.MyLongMethod(ping), Times.Never);
        }

        [Fact]
        public async Task SendAsync_Returns_Response()
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
        public void Send_Returns_Response()
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
        public void Send_Throws_PipelineNotFoundException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m => {
            });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            var actualEx = Record.Exception(() =>  mediator.Send<PingResponse>(ping));

            Assert.NotNull(actualEx);
            Assert.IsType<PipelineNotFoundException>(actualEx);
        }
    }
}