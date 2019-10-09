using System;

namespace FluentMediator
{
    public partial class Mediator
    {
        public void Publish(object request)
        {
            var pipeline = MediatorBuilder.GetPipeline(request);
            pipeline.Publish(GetService, request!);
        }

        public Response Send<Response>(object request)
        {
            var pipeline = MediatorBuilder.GetPipeline(request);
            return pipeline.Send<Response>(GetService, request!);
        }
    }
}