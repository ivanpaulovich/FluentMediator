namespace FluentMediator
{
    public partial class Mediator
    {
        public void Publish(object request)
        {
            var pipeline = PipelineCollection.Get(request);
            pipeline.Publish(GetService, request!);
        }

        public Response Send<Response>(object request)
        {
            var pipeline = PipelineCollection.Get(request);
            return pipeline.Send<Response>(GetService, request!);
        }
    }
}