namespace FluentMediator
{
    public interface IPipelineMediator
    {
        void Publish(object request);
        Response Send<Response>(object request);
    }
}