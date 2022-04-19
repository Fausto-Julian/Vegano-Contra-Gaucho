using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class HealthController
    {
        public float maxHealth { get; private set; }
        public float currentHealth { get; private set; }

        public Action OnDeath;
        public Action<float> OnChangeHealth;

        public HealthController(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void SetHealth(float health)
        {
            if (health > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth = health;
            }
        }

        public void SetDamage(float damage)
        {
            currentHealth -= damage;

            OnChangeHealth?.Invoke(currentHealth);
            if (currentHealth <= 0)
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
