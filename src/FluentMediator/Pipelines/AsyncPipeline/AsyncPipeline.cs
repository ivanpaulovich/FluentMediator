using System;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.AsyncPipeline
{
    internal sealed class AsyncPipeline : IAsyncPipeline
    {
        private readonly IMethodCollection<Method<Func<object, object, Task>>> _methods;
        private readonly IDirectAsync? _direct;

        public AsyncPipeline(IMethodCollection<Method<Func<object, object, Task>>> methods, IDirectAsync? direct, Type requestType, string? name)
        {
            _methods = methods;
            _direct = direct;
            RequestType = requestType;
            Name = name;
        }

        public Type RequestType { get; }
        public string? Name { get; }

        public async Task PublishAsync(GetService getService, object request)
        {
            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, request);
            }
        }

        public async Task<TResult> SendAsync<TResult>(GetService getService, object request)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. SendAsync<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, request);
            }

            return await _direct.SendAsync<TResult>(getService, request!) !;
        }
    }
}