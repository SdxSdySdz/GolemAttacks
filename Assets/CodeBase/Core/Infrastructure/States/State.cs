namespace CodeBase.Core.Infrastructure.States
{
    public abstract class State : IState
    {
        protected StateMachine StateMachine { get; private set; }

        public State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}