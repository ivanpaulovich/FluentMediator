namespace FluentMediator
{
    public interface IPipeline
    {
        void Publish(object request);
        Response Send<Response>(object request);
        IMediator Build();
    }
}