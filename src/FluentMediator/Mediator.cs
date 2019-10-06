using System;

namespace FluentMediator
{
    public class Mediator
    {
        private readonly GetService _getService;
        private PipelineCollection _pipelineCollection;

        public Mediator(GetService getService)
        {
            _getService = getService;
            _pipelineCollection = new PipelineCollection();
        }

        public Pipeline<Request> Pipeline<Request>()
        {
            var pipeline = new Pipeline<Request>(this);
            _pipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public void Publish<Request>(Request request)
        {
            IPipeline pipeline;
            if (_pipelineCollection.Contains<Request>(out pipeline))
            {
                pipeline.Publish(request!);
            }
        }

        internal object GetConcreteHandler(Type handlerType)
        {
            return _getService(handlerType);
        }
    }
}