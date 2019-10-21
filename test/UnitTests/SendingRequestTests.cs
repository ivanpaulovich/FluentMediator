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
    public class SendingRequestTests
    {
        [Fact]
        public void Send_Without_Return_Throws_ReturnFunctionIsNullException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>().Pipeline()
                    .Call<IPingHandler>((handler, req) => handler.MyCustomFooMethod(req))
                    .Call<IPingHandler>((handler, req) => handler.MyCustomBarMethod(req));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");

            var actualEx = Record.Exception(
                //
                // Act
                //
                () => mediator.Send<PingResponse>(ping)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx);
        }

        [Fact]
        public void SendAsync_Without_Return_Throws_ReturnFunctionIsNullException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>().PipelineAsync()
                    .Call<IPingHandler>((handler, req) => handler.MyCustomFooBarAsync(req))
                    .Call<IPingHandler>((handler, req) => handler.MyCustomFooBarAsync(req));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");

            var actualEx = Record.ExceptionAsync(
                //
                // Act
                //
                async () => await mediator.SendAsync<PingResponse>(ping)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx.Result);

            pingHandler.Verify(e => e.MyCustomFooMethod(ping), Times.Never);
            pingHandler.Verify(e => e.MyCustomBarMethod(ping), Times.Never);
        }

        [Fact]
        public void CancellableSendAsync_Without_Return_Throws_ReturnFunctionIsNullException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>().CancellablePipelineAsync()
                    .Call<IPingHandler>((handler, req, ct) => handler.MyCancellableForAsync(req, ct))
                    .Call<IPingHandler>((handler, req, ct) => handler.MyCancellableForAsync(req, ct));
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            var cts = new CancellationTokenSource();

            var ping = new PingRequest("Ping");

            var actualEx = Record.ExceptionAsync(
                //
                // Act
                //
                async () => await mediator.SendAsync<PingResponse>(ping, cts.Token)
            );

            Assert.IsType<ReturnFunctionIsNullException>(actualEx.Result);
        }

        [Fact]
        public async Task SendAsync_Returns_Response()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>().PipelineAsync()
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooBarAsync(req)
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
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>()
                    .Pipeline()
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooMethod(req)
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
        public void Send_Named_PipelineReturns_Response()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>()
                    .Pipeline()
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooMethod(req)
                    );
                m.On<PingRequest>()
                    .Pipeline("Foo")
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooMethod(req)
                    );
            });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            var response = mediator.Send<PingResponse>(ping, "Foo");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Send_Named_PipelineAsync_Returns_Response()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>()
                    .PipelineAsync()
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooBarAsync(req)
                    );
                m.On<PingRequest>()
                    .PipelineAsync("Foo")
                    .Return<PingResponse, IPingHandler>(
                        (handler, req) => handler.MyCustomFooBarAsync(req)
                    );
            });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            var response = await mediator.SendAsync<PingResponse>(ping, "Foo");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task SendCancellable_Named_PipelineReturns_Response()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            {
                m.On<PingRequest>()
                    .CancellablePipelineAsync()
                    .Return<PingResponse, IPingHandler>(
                        (handler, req, ct) => handler.MyCancellableForAsync(req, ct)
                    );
                m.On<PingRequest>()
                    .CancellablePipelineAsync("Foo")
                    .Return<PingResponse, IPingHandler>(
                        (handler, req, ct) => handler.MyCancellableForAsync(req, ct)
                    );
            });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            var cts = new CancellationTokenSource();

            var ping = new PingRequest("Ping");
            var response = await mediator.SendAsync<PingResponse>(ping, cts.Token, "Foo");

            Assert.NotNull(response);
        }

        [Fact]
        public void Send_Not_Configured_Throws_PipelineNotFoundException()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            { });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var ping = new PingRequest("Ping");
            var actualEx = Record.Exception(() => mediator.Send<PingResponse>(ping));

            Assert.NotNull(actualEx);
            Assert.IsType<PipelineNotFoundException>(actualEx);
        }

        [Fact]
        public void Send_Throws_Exception_Null_Requests()
        {
            var services = new ServiceCollection();
            services.AddFluentMediator(m =>
            { });

            services.AddScoped<IPingHandler, PingHandler>();
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var actualEx = Record.Exception(() => mediator.Send<PingResponse>(null!));

            Assert.NotNull(actualEx);
            Assert.IsType<NullRequestException>(actualEx);
        }
    }
}