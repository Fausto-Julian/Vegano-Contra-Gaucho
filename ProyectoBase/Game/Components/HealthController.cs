using System;

namespace Game.Components
{
    public class HealthController : Component
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }

        public event Action OnDeath;

        public HealthController(GameObject gameObject, float maxHealth)
            : base(gameObject)
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
    }
}
