namespace FluentMediator
{
    public partial class Mediator
    {
        public PipelineCollection<IPipeline> PipelineCollection { get; }

        public Pipeline<Request> Pipeline<Request>()
        {
            var pipeline = new Pipeline<Request>(this);
            PipelineCollection.Add<Request>(pipeline);
            return pipeline;
        }

        public void Publish<Request>(Request request)
        {
            if (PipelineCollection.Contains<Request>(out var pipeline))
            {
                pipeline?.Publish(request!);
            }
        }
    }
}