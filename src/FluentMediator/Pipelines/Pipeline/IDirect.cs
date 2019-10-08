namespace FluentMediator
{
    public interface IDirect
    {
        object Send(GetService getService, object request);
        Response Send<Response>(GetService getService, object request);
    }
}