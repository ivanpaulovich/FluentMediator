namespace FluentMediator
{
    public interface IMediator :
        Pipelines.Pipeline.IMediator,
        Pipelines.PipelineAsync.IMediator,
        Pipelines.CancellablePipelineAsync.IMediator { }
}