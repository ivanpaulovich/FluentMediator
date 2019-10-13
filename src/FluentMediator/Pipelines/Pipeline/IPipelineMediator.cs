namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineMediator
    {
        void Publish(object request);
        TResult Send<TResult>(object request);
    }
}