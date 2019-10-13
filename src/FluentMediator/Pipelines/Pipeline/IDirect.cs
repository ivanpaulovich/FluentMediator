namespace FluentMediator.Pipelines.Pipeline
{
    public interface IDirect
    {
        TResult Send<TResult>(GetService getService, object request);
    }
}