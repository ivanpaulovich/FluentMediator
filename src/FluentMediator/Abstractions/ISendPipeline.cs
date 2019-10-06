namespace FluentMediator
{
    public interface ISendPipeline 
    {
        object Send(object request);
    }
}