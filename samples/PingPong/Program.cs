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
            serviceCollection.AddFluentMediator();
            serviceCollection.AddScoped<PingHandler>();

            var provider = serviceCollection.BuildServiceProvider();
            var mediator = provider.GetService<Mediator>();

            mediator.Pipeline<PingRequest>()
                .Handler<PingHandler>((handler, req) => handler.MyMethod(req))
                .Handler<PingHandler>((handler, req) => handler.MyLongMethod(req));
            mediator.AsyncPipeline<PingRequest>()
                .HandlerAsync<PingHandler>(async (handler, req) => await handler.MyMethodAsync(req));
            mediator.CancellablePipeline<PingRequest>()
                .HandlerAsync<PingHandler>(async (handler, req, ct) => await handler.MyMethodAsync(req, ct));

            var ping = new PingRequest("Ping");
            var cts = new CancellationTokenSource();

            Console.WriteLine("Publishing Ping. Should Pong Twice.");
            
            mediator.Publish<PingRequest>(ping);

            Console.WriteLine("Publishing Ping Async. Should Pong One.");

            mediator.PublishAsync<PingRequest>(ping)
                .GetAwaiter()
                .GetResult();

            Console.WriteLine("Publishing Cancellable Ping. Should Pong One.");

            mediator.PublishAsync<PingRequest>(ping, cts.Token)
                .GetAwaiter()
                .GetResult();

            Console.ReadLine();
        }
    }
}
