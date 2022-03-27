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

        public EnemyTest(string id, float maxHealth, Animation animation, Vector2 startPosition):
            base(id, animation, startPosition, Vector2.One)
        {
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;
        }

        public void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);
        }

        void IHealthController.GetDamage(float damage)
        {
            healthController.GetDamage(damage);
        }
    }
}
