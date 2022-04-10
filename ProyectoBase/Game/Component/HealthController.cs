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

        public event Action OnDeath;

        public HealthController(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void GetDamage(float damage)
        {
            currentHealth -= damage;
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
