using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private Collider2D _zoneCollider;
        [SerializeField] private float _cooldown;

        private ContactFilter2D _contactFilter;
        private List<Collider2D> _results;
        private float _timer;
        private Golem _golem;
        private bool _isOnCooldown;

        public void Construct(Golem golem)
        {
            _golem = golem;
        }

        private void Awake()
        {
            _results = new List<Collider2D>();
            _contactFilter = new ContactFilter2D
            {
                useTriggers = true
            };
        }

        private void Update()
        {
            if (_golem == null)
                return;

            if (_isOnCooldown)
            {
                _timer += Time.deltaTime;

                if (_timer >= _cooldown)
                {
                    _timer = 0f;
                    _isOnCooldown = false;
                }

                return;
            }

            if (HasReachedZombies())
            {
                ApplyDamage();
                _isOnCooldown = true;
            }
        }

        private bool HasReachedZombies()
        {
            _results.Clear();
            _zoneCollider.OverlapCollider(_contactFilter, _results);

            foreach (var collider in _results)
            {
                if (collider.TryGetComponent(out Zombie zombie))
                    return true;
            }

            return false;
        }

        private void ApplyDamage()
        {
            int damage = CalculateDamage();
            _golem.TakeDamage(damage);
        }

        private int CalculateDamage()
        {
            int totalDamage = 0;

            foreach (var collider in _results)
            {
                if (collider.TryGetComponent(out Zombie zombie))
                {
                    totalDamage += zombie.Damage;
                }
            }

            return totalDamage;
        }
    }
}
