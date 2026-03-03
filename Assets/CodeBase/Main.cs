using System;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.Services;
using CodeBase.Core.Infrastructure.Services.Facroty.Enemies.ForZombie;
using CodeBase.Core.Infrastructure.Services.GamePlatform;
using CodeBase.Core.Infrastructure.Services.Input;
using CodeBase.Core.Infrastructure.Services.Localization;
using CodeBase.Core.Infrastructure.Services.Sequence;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.States;
using Lean.Localization;
using UnityEngine;
using YG;

namespace CodeBase
{
    public class Main : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LeanLocalization _localization;
        [SerializeField] private ZombieFactory _zombieFactory;
        
        private readonly AllServices _services = AllServices.Container;
        
        private void Awake()
        {
            // AudioListener.pause = true;
            
            var stateMachine = new StateMachine();
            var sceneLoader = new SceneLoader(coroutineRunner: this);

            RegisterServices();
            RegisterStates(stateMachine, sceneLoader);

            stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void RegisterServices()
        {
            _services.Register<ILocalizationService>(new LocalizationService(_localization));
            _services.Register<IGamePlatform>(new YandexGamesPlatform());
            _services.Register<IInputService>(GetInputService());
            _services.Register<ISequenceService>(new SequenceService(coroutineRunner: this));
            _services.Register<IZombieFactory>(_zombieFactory);
        }

        private void RegisterStates(StateMachine stateMachine, SceneLoader sceneLoader)
        {
            stateMachine.Register(new BootstrapState(sceneLoader, stateMachine));
            
            stateMachine.Register(new DetermineLanguageState(
                _services.Get<IGamePlatform>(), 
                _services.Get<ILocalizationService>(), 
                stateMachine));
            
            stateMachine.Register(new LoadMenuState(
                _services.Get<IInputService>(), 
                sceneLoader,
                stateMachine));
            
            stateMachine.Register(new ShowMenuState(stateMachine));
            
            stateMachine.Register(new LoadBattleSceneState(
                _services.Get<IGamePlatform>(),
                _services.Get<IInputService>(),
                _services.Get<ISequenceService>(),
                _services.Get<IZombieFactory>(),
                sceneLoader,
                stateMachine));
            
            stateMachine.Register(new RunBattleState(stateMachine));
            
            stateMachine.Register(new DeathState(stateMachine));
        }

        private IInputService GetInputService()
        {
            switch (YG2.envir.device)
            {
                case YG2.Device.Desktop:
                    return new StandaloneInputService();
                case YG2.Device.Mobile:
                    return new MobileInputService();
                case YG2.Device.Tablet:
                    return new MobileInputService();
                case YG2.Device.TV:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}