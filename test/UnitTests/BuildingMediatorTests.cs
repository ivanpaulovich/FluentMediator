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
            Exception ex = Record.Exception(() =>
            {
                services.AddFluentMediator(m =>
                {
                    m.On<PingRequest>().AsyncPipeline()
                        .Return<PingResponse, IPingHandler>(
                            (handler, req) => handler.MyCustomFooBarAsync(req)
                        );

                    m.On<PingRequest>().AsyncPipeline()
                        .Return<PingResponse, IPingHandler>(
                            (handler, req) => handler.MyCustomFooBarAsync(req)
                        );

                });
            });

            Assert.NotNull(ex);
            Assert.IsType<PipelineAlreadyExistsException>(ex);
        }
    }
}