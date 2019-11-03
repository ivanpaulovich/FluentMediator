namespace FluentMediator.Pipelines.Pipeline
{
    internal interface IDirect
    {
        TResult Send<TResult>(
            GetService getService,
            object request
        );
    }
}