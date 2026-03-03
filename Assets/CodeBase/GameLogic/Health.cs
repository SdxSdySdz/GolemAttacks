using System;

namespace CodeBase.GameLogic
{
    public class Health
    {
        private readonly int _maxHealth;
        
        private int _health;

        public bool IsDead => _health <= 0;

        public Health(int maxHealth)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
        }

        public event Action<int, int> Changed;
        public event Action Dead;

        public void ApplyDamage(int damage)
        {
            _health -= damage;
          
            Changed?.Invoke(_health, _maxHealth);
            
            if (_health <= 0)
                Dead?.Invoke();
        }
    }
}