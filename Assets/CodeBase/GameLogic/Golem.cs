using CodeBase.Core.Infrastructure.Services.Input;
using CodeBase.Core.Infrastructure.Services.Sequence;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class Golem : MonoBehaviour
    {
        private const float AttackDuration = 0.25f;
        private const float RelaxDuration = 0.5f;

        [SerializeField] private GolemSounds _sounds;
        [SerializeField] private Transform _zombieTarget;
        [SerializeField] private KickZone _kickZone;
        [SerializeField] private DamageZone _damageZone;
        [SerializeField] private Body _body;

        private IInputService _inputService;
        private ISequenceService _sequenceService;
        private bool _isListeningInput;
        private Health _health;

        private float Cooldown => AttackDuration + RelaxDuration;
        public Transform ZombieTarget => _zombieTarget;
        public Health Health => _health;

        public void Construct(IInputService inputService, ISequenceService sequenceService)
        {
            _inputService = inputService;
            _sequenceService = sequenceService;
            _isListeningInput = true;

            _health = new Health(20);
            _damageZone.Construct(this);
            _body.Construct();

            _body.Show();
        }

        public void Destruct()
        {
            _inputService = null;
            _sequenceService = null;
            _isListeningInput = false;

            _body.Hide();
        }

        private void Update()
        {
            if (_inputService == null || !_inputService.IsClicked())
                return;

            if (_isListeningInput)
                Attack();
        }

        public void Attack()
        {
            _body.Attack();
            ApplyCooldown();
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            _sequenceService.CallLater(_kickZone.Kick, AttackDuration * 0.3f);
        }

        private void ApplyCooldown()
        {
            DontListenInput();
            _sequenceService.CallLater(ListenInput, Cooldown);
        }

        private void ListenInput() => _isListeningInput = true;
        private void DontListenInput() => _isListeningInput = false;

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            _health.ApplyDamage(damage);

            if (_health.IsDead)
                _sounds.PlayKill();
            else
            {
                _sounds.PlayTakeDamage();
                _body.FlashRedTwice();
            }
        }
    }
}
