using System;
using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;
using CodeBase.GameUI;
using UnityEngine;
using YG;
using YG.Utils.LB;
using Object = UnityEngine.Object;

namespace CodeBase.GameLogic.States
{
    public class DeathState : IndependentState, IExitableState
    {
        private GolemScene _scene;
        
        public DeathState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            _scene = Object.FindObjectOfType<GolemScene>();
            
            _scene.Spawner.Clear();
            _scene.Golem.Destruct();

            _scene.DeathWindow.Show(onRestartRequested: Restart, onMenuRequested: ToMenu);
            _scene.DeathAudioSource.Play(1);
            
            YG2.onGetLeaderboard += TryUpdateScore;
            YG2.GetLeaderboard("MenuLeaderboard");
        }

        public void Exit()
        {
            _scene.DeathWindow.Hide();
        }

        private void Restart()
        {
            StateMachine.Enter<LoadBattleSceneState>();
        }

        private void ToMenu()
        {
            StateMachine.Enter<LoadMenuState>();
        }

        private void TryUpdateScore(LBData data)
        {
            int score = _scene.Score.Value;
            if (data.currentPlayer.score < score)
                YG2.SetLeaderboard("MenuLeaderboard", score);
            
            YG2.onGetLeaderboard -= TryUpdateScore;
        }
    }
}