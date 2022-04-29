using System;
using Game.Interface;

namespace Game.Component
{
    public class HealthController
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }

        public event Action OnDeath;
        public event Action<float> OnChangeHealth;

        public HealthController(float maxHealth)
        {
            this.MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void SetHealth(float health)
        {
            CurrentHealth = health > MaxHealth ? MaxHealth : health;
        }

        public void SetDamage(float damage)
        {
            Engine.Debug("dañoooo");
            CurrentHealth -= damage;

            OnChangeHealth?.Invoke(CurrentHealth);
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
