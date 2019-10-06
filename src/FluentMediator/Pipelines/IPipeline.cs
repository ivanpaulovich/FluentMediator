namespace FluentMediator.Pipeline
{
    public interface IPipeline
    {
        void Publish(object request);
    }
}