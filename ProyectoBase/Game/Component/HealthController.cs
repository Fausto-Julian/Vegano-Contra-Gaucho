using System;
using Game.Interface;

namespace Game.Component
{
    public class HealthController
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }

        public event Action OnDeath;

        public HealthController(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void SetHealth(float health)
        {
            CurrentHealth = health > MaxHealth ? MaxHealth : health;
        }

        public void SetDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                OnDeath.Invoke();
            }
        }

        public static bool operator ==(HealthController a, HealthController b)
        {
            return a == b;
        }

        public static bool operator !=(HealthController a, HealthController b)
        {
            return a != b;
        }
    }
}
