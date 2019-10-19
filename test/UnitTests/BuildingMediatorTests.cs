using System;
using FluentMediator;
using FluentMediator.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.PingPong;
using Xunit;

namespace UnitTests
{
    public class BuildPipelineTests
    {
        [Fact]
        public void BuildSendAsyncPipeline_ThrowsPipelineAlreadyExistsException()
        {
            var services = new ServiceCollection();
            
            var pipelineProviderBuilder = new PipelineProviderBuilder();

            pipelineProviderBuilder.On<PingRequest>().AsyncPipeline()
                .Return<PingResponse, IPingHandler>(
                    (handler, req) => handler.MyCustomFooBarAsync(req)
                );

            pipelineProviderBuilder.On<PingRequest>().AsyncPipeline()
                .Return<PingResponse, IPingHandler>(
                    (handler, req) => handler.MyCustomFooBarAsync(req)
                );

            Exception ex = Record.Exception(() => pipelineProviderBuilder.Build());

            Assert.NotNull(ex);
            Assert.IsType<PipelineAlreadyExistsException>(ex);
        }
    }
}