namespace FluentMediator.Pipelines
{
    /// <summary>
    /// Named Pipeline
    /// </summary>
    public interface INamedPipeline
    {
        /// <summary>
        /// An unique pipeline name
        /// </summary>
        /// <value>null</value>
        string? Name { get; }
    }
}