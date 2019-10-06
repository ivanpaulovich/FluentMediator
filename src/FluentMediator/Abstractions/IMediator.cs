namespace FluentMediator
{
    public interface IMediator : IAsyncMediator, ICancellableMediator
    {
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}