using System;

namespace FluentMediator
{
    public partial class Mediator
    {
        public void Publish<Request>(Request request)
        {
            if (PipelinesManager.PipelineCollection.Contains<Request>(out var pipeline))
            {
                pipeline?.Publish(GetService, request!);
            }
        }

        public Response Send<Response>(object request)
        {
            if (PipelinesManager.PipelineCollection.Contains(request.GetType(), out var pipeline))
            {
                if (pipeline is IPipeline)
                {
                    return pipeline.Send<Response>(GetService, request!);
                }
            }

            throw new Exception("Send Pipeline Not Found.");
        }
    }
}