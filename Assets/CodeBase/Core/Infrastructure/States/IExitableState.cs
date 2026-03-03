namespace CodeBase.Core.Infrastructure.States
{
    public interface IExitableState : IState
    {
        void Exit();
    }
}