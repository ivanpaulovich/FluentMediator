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
    }
}