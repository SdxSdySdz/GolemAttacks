using System;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.Services.Input;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;
using Object = UnityEngine.Object;

namespace CodeBase.GameLogic.States
{
    public class LoadMenuState : IndependentState
    {
        private readonly IInputService _inputService;
        private readonly SceneLoader _sceneLoader;

        public LoadMenuState(IInputService inputService, SceneLoader sceneLoader, StateMachine stateMachine) : base(stateMachine)
        {
            _inputService = inputService;
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.Load<MenuScene>(onLoaded: InitScene);
        }

        private void InitScene()
        {
            Object.FindObjectOfType<MenuScene>().Menu.Construct(_inputService);
            StateMachine.Enter<ShowMenuState>();
        }
    }
}