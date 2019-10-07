namespace FluentMediator
{
    public interface IDirect
    {
        object Send(object request);
        Response Send<Response>(object request);
    }
}