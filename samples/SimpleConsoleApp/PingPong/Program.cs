using System;
using System.Threading;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;

namespace PingPong
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFluentMediator(builder =>
            {
                builder.On<PingRequest>().Pipeline()
                    .Call<PingHandler>((handler, req) => handler.MyMethod(req))
                    .Call<PingHandler>((handler, req) => handler.MyLongMethod(req));
                builder.On<PingRequest>().PipelineAsync()
                    .Call<PingHandler>(async(handler, req) => await handler.MyMethodAsync(req));
                builder.On<PingRequest>().CancellablePipelineAsync()
                    .Call<PingHandler>(async(handler, req, ct) => await handler.MyMethodAsync(req, ct));
            });
            serviceCollection.AddScoped<PingHandler>();

            var provider = serviceCollection.BuildServiceProvider();
            var mediator = provider.GetService<IMediator>();

            var ping = new PingRequest("Ping");
            var cts = new CancellationTokenSource();

            Console.WriteLine("Publishing Ping. Should Pong Twice.");

            mediator.Publish(ping);

            Console.WriteLine("Publishing Ping Async. Should Pong One.");

            mediator.PublishAsync(ping)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine("Publishing Cancellable Ping. Should Pong One.");

            mediator.PublishAsync(ping, cts.Token)
                .GetAwaiter()
                .GetResult();

            Console.ReadLine();
        }
    }
}