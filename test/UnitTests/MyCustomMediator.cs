using FluentMediator;

namespace UnitTests
{
    public class MyCustomMediator : Mediator
    {
        public bool MyCustomPipelineNotFoundHandlerWasCalled = false;

        public MyCustomMediator(GetService getService, IPipelineProvider pipelines) : base(getService, pipelines)
        { }

        protected override void OnPipelineNotFound(PipelineNotFoundEventArgs e)
        {
            MyCustomPipelineNotFoundHandlerWasCalled = true;

            //Do something before raising the event
            base.OnPipelineNotFound(e);
            //Do something after raising the event 
        }
    }
}