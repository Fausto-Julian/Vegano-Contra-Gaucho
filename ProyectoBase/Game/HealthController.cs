using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class HealthController
    {
        private float MaxHealth;

        public event Action OnDeath;

        public float currentHealth { get; private set; }


        public HealthController(float maxHealth)
        {
            MaxHealth = maxHealth;
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


    }
}
