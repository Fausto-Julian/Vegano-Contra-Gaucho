using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyTest : GameObject, IHealthController
    {
        public HealthController HealthController { get; private set; }

        public Action OnDesactive;

        public EnemyTest(string id, float maxHealth, Texture texture, Vector2 startPosition):
            base(id, new Texture("test.png"), startPosition, new Vector2(0.5f, 0.5f))
        {
            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += DeathHandler;
        }

        public void Initialize(Vector2 newPosition, float health)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(health);
        }

        void IHealthController.SetDamage(float damage)
        {
            HealthController.SetDamage(damage);
        }

        private void DeathHandler()
        {
            OnDesactive?.Invoke();
        }
    }
}
