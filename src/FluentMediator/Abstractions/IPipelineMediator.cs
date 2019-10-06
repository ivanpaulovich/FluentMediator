namespace FluentMediator
{
    public interface IPipelineMediator
    {
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}