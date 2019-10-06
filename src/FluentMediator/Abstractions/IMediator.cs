namespace FluentMediator
{
    public interface IMediator : IAsyncMediator, ICancellableMediator, ISendMediator
    {
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}