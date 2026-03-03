using CodeBase.Core.Audio;
using CodeBase.Core.Infrastructure.Scenes;
using CodeBase.GameLogic.States;
using CodeBase.GameUI;

namespace CodeBase.GameLogic.Scenes
{
    public class GolemScene : Scene
    {
        public HealthBar HealthBar;
        public Golem Golem;
        public CustomAudioSource BattleAudioSource;
        public CustomAudioSource DeathAudioSource;
        public VignetteEffect VignetteEffect;
        public DeathWindow DeathWindow;
        public ZombieSpawner Spawner;
        public Score Score;
    }
}