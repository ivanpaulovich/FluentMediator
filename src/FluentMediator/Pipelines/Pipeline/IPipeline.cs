namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipeline
    {
        void Publish(GetService getService, object request);
        Response Send<Response>(GetService getService, object request);
        MediatorBuilder Build();
    }
}