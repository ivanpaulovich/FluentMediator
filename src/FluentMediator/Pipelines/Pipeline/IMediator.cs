namespace FluentMediator.Pipelines.Pipeline
{
    public interface IMediator
    {
        void Publish(object request);
        TResult Send<TResult>(object request);
    }
}