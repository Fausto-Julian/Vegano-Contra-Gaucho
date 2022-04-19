using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyTest : GameObject, IHealthController
    {
        public HealthController healthController { get; private set; }

        public Action OnDesactive;

        public EnemyTest(string id, float maxHealth, Texture texture, Vector2 startPosition):
            base(id, texture, startPosition, Vector2.One)
        {
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += DeathHandler;
        }

        public void Initialize(Vector2 newPosition, float health)
        {
            transform.Position = newPosition;
            healthController.SetHealth(health);
        }

        void IHealthController.GetDamage(float damage)
        {
            healthController.SetDamage(damage);
        }

        private void DeathHandler()
        {
            OnDesactive?.Invoke();
        }
    }
}
