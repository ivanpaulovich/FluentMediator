namespace FluentMediator
{
    public interface IMediator : IAsyncMediator, ICancellableMediator, ISendMediator, ISendAsyncMediator
    {
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}