using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;

namespace CodeBase.GameLogic.States
{
    public class BootstrapState : IndependentState
    {
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(SceneLoader sceneLoader, StateMachine stateMachine) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.Load<MainScene>(onLoaded: LoadGolemState);
        }

        private void LoadGolemState()
        {
            StateMachine.Enter<DetermineLanguageState>();
        }
    }
}