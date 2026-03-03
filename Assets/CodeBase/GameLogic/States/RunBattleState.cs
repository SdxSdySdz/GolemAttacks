using CodeBase.Core.Infrastructure.States;
using CodeBase.GameLogic.Scenes;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class RunBattleState : IndependentState    
    {
        private const float MusicFadingDuration = 5f;
        

        private GolemScene _scene;

        public RunBattleState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            _scene = Object.FindObjectOfType<GolemScene>();
            
            _scene.Golem.Attack();
            _scene.HealthBar.Show();

            SpawnEnemies();

            StartBattleMusic();

            _scene.Golem.Health.Dead += ToDeathState;
        }

        private void SpawnEnemies()
        {
            _scene.Spawner.StartSpawning();
        }

        private void StartBattleMusic()
        {
            _scene.BattleAudioSource.Play(MusicFadingDuration);
        }

        private void ToDeathState()
        {
            _scene.BattleAudioSource.Stop(1f);
            StateMachine.Enter<DeathState>();
        }
    }
}