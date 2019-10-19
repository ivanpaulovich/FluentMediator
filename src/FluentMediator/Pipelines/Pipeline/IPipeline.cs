namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipeline : INamedPipeline, ITypedPipeline
    {
        void Publish(GetService getService, object request);
        TResult Send<TResult>(GetService getService, object request);
    }
}