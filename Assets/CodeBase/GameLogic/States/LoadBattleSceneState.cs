using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.Services.Facroty.Enemies.ForZombie;
using CodeBase.Core.Infrastructure.Services.GamePlatform;
using CodeBase.Core.Infrastructure.Services.Input;
using CodeBase.Core.Infrastructure.Services.Sequence;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class LoadBattleSceneState : IndependentState
    {
        private readonly IGamePlatform _platform;
        private readonly IInputService _inputService;
        private readonly ISequenceService _sequenceService;
        private readonly IZombieFactory _zombieFactory;
        private readonly SceneLoader _sceneLoader;

        private GolemScene _scene;
        private int _playCount;
        private int _advertisementCooldown = 3;

        public LoadBattleSceneState(
            IGamePlatform platform,
            IInputService inputService, 
            ISequenceService sequenceService, 
            IZombieFactory zombieFactory,
            SceneLoader sceneLoader,
            StateMachine stateMachine) : base(stateMachine)
        {
            _platform = platform;
            _inputService = inputService;
            _sequenceService = sequenceService;
            _zombieFactory = zombieFactory;
            _sceneLoader = sceneLoader;
            _playCount = 0;
        }

        public override void Enter()
        {
            _playCount++;

            if (_playCount >= _advertisementCooldown)
            {
                _platform.ShowInterstitialAdvertisement();
                _playCount = 0;
            }

            _sceneLoader.Load<GolemScene>(doReloadOnSameScene: true, onLoaded: InitScene);
        }

        private void InitScene()
        {
            _scene = Object.FindObjectOfType<GolemScene>();

            _scene.Golem.Construct(_inputService, _sequenceService);
            _scene.HealthBar.BindHealth(_scene.Golem.Health);
            
            _scene.VignetteEffect.Construct(_scene.Golem.Health);
            
            _scene.Spawner.Construct(_scene.Golem, _zombieFactory);
            
            StateMachine.Enter<RunBattleState>();
        }
    }
}