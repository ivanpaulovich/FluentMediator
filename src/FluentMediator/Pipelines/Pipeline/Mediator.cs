using System;

namespace FluentMediator
{
    public partial class Mediator
    {
        public PipelineCollection<IPipeline> PipelineCollection { get; }

        public void Publish<Request>(Request request)
        {
            if (PipelineCollection.Contains<Request>(out var pipeline))
            {
                pipeline?.Publish(request!);
            }
        }

        public Response Send<Response>(object request)
        {
            if (PipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                if (!(pipeline is null))
                {
                    return pipeline.Send<Response>(request!);
                }
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}