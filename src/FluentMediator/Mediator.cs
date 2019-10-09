namespace FluentMediator
{
    public partial class Mediator : IMediator
    {
        public GetService GetService { get; }
        public MediatorBuilder MediatorBuilder { get; }

        public Mediator(GetService getService, MediatorBuilder mediatorBuilder)
        {
            GetService = getService;
            MediatorBuilder = mediatorBuilder;
        }
    }
}