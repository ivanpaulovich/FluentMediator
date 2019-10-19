using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMediator.Pipelines.CancellablePipelineAsync
{
    internal sealed class Pipeline : ICancellablePipelineAsync
    {
        private readonly IMethodCollection<Method<Func<object, object, CancellationToken, Task>>> _methods;
        private readonly IDirect? _direct;

        public Pipeline(IMethodCollection<Method<Func<object, object, CancellationToken, Task>>> methods, IDirect? direct, Type requestType, string? name)
        {
            _methods = methods;
            _direct = direct;
            RequestType = requestType;
            Name = name;
        }

        public Type RequestType { get; }
        public string? Name { get; }

        public async Task PublishAsync(GetService getService, object request, CancellationToken cancellationToken)
        {
            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, request, cancellationToken);
            }
        }

        public async Task<TResult> SendAsync<TResult>(GetService getService, object request, CancellationToken cancellationToken)
        {
            if (_direct is null)
            {
                throw new ReturnFunctionIsNullException("The return function is null. SendAsync<TResult> method not executed.");
            }

            foreach (var handler in _methods.GetMethods())
            {
                var concreteHandler = getService(handler.HandlerType);
                await handler.Action(concreteHandler, request, cancellationToken);
            }

            return await _direct.SendAsync<TResult>(getService, request!, cancellationToken) !;
        }
    }
}