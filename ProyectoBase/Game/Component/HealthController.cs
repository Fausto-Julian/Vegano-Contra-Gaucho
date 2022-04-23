using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class HealthController
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public Action OnDeath;
        public Action<float> OnChangeHealth;

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
            CurrentHealth -= damage;

            OnChangeHealth?.Invoke(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
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
