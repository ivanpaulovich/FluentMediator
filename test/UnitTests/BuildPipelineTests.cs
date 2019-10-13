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
    public class BuildPipelineTests
    {
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
                Assert.IsType<PipelineAlreadyExistsException>(ex);
            });
            var pingHandler = new Mock<IPingHandler>();
            services.AddScoped<IPingHandler>(provider => pingHandler.Object);

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
        }
    }
}