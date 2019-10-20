namespace FluentMediator.Pipelines.Pipeline
{
    public interface IMediator
    {
        void Publish(object request, string? pipelineName = null);
        TResult Send<TResult>(object request, string? pipelineName = null);
    }
}