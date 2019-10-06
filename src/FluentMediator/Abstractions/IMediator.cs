namespace FluentMediator
{
    public interface IMediator
    {
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}