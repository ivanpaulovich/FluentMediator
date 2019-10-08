namespace FluentMediator
{
    public interface IPipeline
    {
        void Publish(GetService getService, object request);
        Response Send<Response>(GetService getService, object request);
        PipelinesManager Build();
    }
}