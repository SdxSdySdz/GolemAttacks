namespace CodeBase.Core.Infrastructure.States
{
    public abstract class DependentState<TDependency> : State
    {
        protected DependentState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public abstract void Enter(TDependency dependency);
    }
}