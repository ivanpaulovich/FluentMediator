namespace FluentMediator
{
    public interface IPipelineMediator
    {
        void Publish<Request>(Request request);
        Response Send<Response>(object request);
    }
}