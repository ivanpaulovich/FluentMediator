namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        public GetService GetService { get; }
        public PipelinesManager PipelinesManager { get; }

        public Mediator(GetService getService, PipelinesManager pipelinesManager)
        {
            GetService = getService;
            PipelinesManager = pipelinesManager;
        }

        public PipelineBuilder<Request> When<Request>()
        {
            return new PipelineBuilder<Request>(PipelinesManager);
        }
    }
}