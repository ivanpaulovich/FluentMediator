namespace FluentMediator
{
    public interface IPipeline
    {
        void Publish(object request);
    }
}