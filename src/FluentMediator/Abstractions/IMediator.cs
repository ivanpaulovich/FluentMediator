namespace FluentMediator
{
    public interface IMediator : IDirectMediator, IDirectAsyncMediator, IPipelineMediator, IAsyncPipelineMediator, ICancellablePipelineMediator
    { }
}