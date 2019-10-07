namespace FluentMediator
{
    public interface IMediator : IDirectMediator, IDirectAsyncMediator, IPipelineMediator, IAsyncPipelineMediator, ICancellablePipelineMediator
    {
        GetService GetService { get; }
        PipelineBuilder<Request> For<Request>();
    }
}