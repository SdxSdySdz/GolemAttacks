using System;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;
using YG;
using Object = UnityEngine.Object;

namespace CodeBase.GameLogic.States
{
    public class ShowMenuState : IndependentState, IExitableState
    {
        private const float MusicFadingInDuration = 5f;
        private const float MusicFadingOutDuration = 2f;
        
        private MenuScene _scene;

        public ShowMenuState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            _scene = Object.FindObjectOfType<MenuScene>();
            
            ShowMenu();
            PlayMusic();
        }

        public void Exit()
        {
            _scene.Menu.Hide();
        }

        private void ShowMenu()
        {
            _scene.Menu.Show();
            _scene.Menu.PlaySelected += PrepareGameRunning;
        }

        private void PlayMusic()
        {
            _scene.MenuAudioSource.Play(fadingDuration: MusicFadingInDuration);
        }

        private void PrepareGameRunning()
        {
            StopMusic(ToRunGameState);
        }

        private void StopMusic(Action onFinished)
        {
            Object.DontDestroyOnLoad(_scene.MenuAudioSource);
            _scene.MenuAudioSource.Stop(MusicFadingOutDuration);
            onFinished?.Invoke();
        }

        private void ToRunGameState()
        {
            StateMachine.Enter<LoadBattleSceneState>();
        }
    }
}