namespace FluentMediator
{
    public interface IPipelineMediator
    {
        PipelineCollection<IPipeline> PipelineCollection { get; }
        void Publish<Request>(Request request);
        Response Send<Response>(object request);
    }
}