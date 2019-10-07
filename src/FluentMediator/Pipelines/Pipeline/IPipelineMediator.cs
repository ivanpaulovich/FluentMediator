namespace FluentMediator
{
    public interface IPipelineMediator
    {
        PipelineCollection<IPipeline> PipelineCollection { get; }
        Pipeline<Request> Pipeline<Request>();
        void Publish<Request>(Request request);
    }
}