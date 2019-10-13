namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipeline
    {
        void Publish(GetService getService, object request);
        TResult Send<TResult>(GetService getService, object request);
        MediatorBuilder Build();
    }
}