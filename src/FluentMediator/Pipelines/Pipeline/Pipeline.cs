using System;

namespace FluentMediator
{
    public class Pipeline<Request> : IPipeline
    {
        private readonly PipelinesManager _pipelinesManager;
        private readonly MethodCollection<Method<Action<object, Request>, Request>, Request > _methods;
        private IDirect _direct;

        public Pipeline(PipelinesManager pipelinesManager)
        {
            _pipelinesManager = pipelinesManager;
            _methods = new MethodCollection<Method<Action<object, Request>, Request>, Request > ();
            _direct = null!;
        }

        public Pipeline<Request> Call<Handler>(Action<Handler, Request> action)
        {
            Action<object, Request> typedHandler = (h, r) => action((Handler) h, (Request) r);
            var method = new Method<Action<object, Request>, Request>(typeof(Handler), typedHandler);
            _methods.Add(method);
            return this;
        }

        public IDirect Return<Response, Handler>(Func<Handler, Request, Response> func)
        {
            _direct = new Direct<Request, Response, Handler>(func);
            return _direct;
        }

        public void Publish(GetService getService, object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, (Request) request);
            }
        }

        public Response Send<Response>(GetService getService, object request)
        {
            foreach (var handler in _methods.GetHandlers())
            {
                var concreteHandler = getService(handler.HandlerType);
                handler.Action(concreteHandler, (Request) request);
            }

            if (_direct is null)
                throw new Exception("Send not configured.");

            return _direct.Send<Response>(getService, request!) !;
        }

        public PipelinesManager Build()
        {
            return _pipelinesManager;
        }
    }
}