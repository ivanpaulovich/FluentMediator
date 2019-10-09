namespace FluentMediator.Pipelines.Pipeline
{
    public interface IPipelineMediator
    {
        void Publish(object request);
        Response Send<Response>(object request);
    }
}