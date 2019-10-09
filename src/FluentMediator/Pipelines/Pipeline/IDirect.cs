namespace FluentMediator.Pipelines.Pipeline
{
    public interface IDirect
    {
        Response Send<Response>(GetService getService, object request);
    }
}