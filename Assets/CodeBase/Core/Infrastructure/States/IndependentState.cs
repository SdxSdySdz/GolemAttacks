namespace CodeBase.Core.Infrastructure.States
{
    public abstract class IndependentState : State
    {
        protected IndependentState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public abstract void Enter();
    }
}