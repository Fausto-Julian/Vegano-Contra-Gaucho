using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyBasic : GameObject, IHealthController
    {
        private bool movingright;
        
        private float currentTimeToShoot;
        private float coolDownShoot;

        private readonly ShootController shootController;

        private HealthController HealthController { get; set; }

        public event Action OnDesactive;
        
        public EnemyBasic(string id, Texture texture, Vector2 startPosition, float maxHealth)
            : base(id, texture, startPosition, Vector2.One, true)
        {
            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += DeathHandler;
            shootController = new ShootController(id, "Texture/molly.png", 250f, 20f, false);
        }
        
        public void Initialize(Vector2 newPosition, float health)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(health);
        }

        public override void Update()
        {
            ShootPlayer();
            if (Transform.Position.X >= Program.WINDOW_WIDTH)
            {
                movingright = false;
            }
            if (Transform.Position.X <= 0)
            {
                movingright = true;
            }
            
            switch (movingright)
            {
                case true:
                    Transform.Position.X += 2;
                    break;
                case false:
                    Transform.Position.X -= 2;
                    break;
            }
            base.Update();
        }
        
        private void DeathHandler()
        {
            OnDesactive.Invoke();
        }
        
        public void SetDamage(float damage)
        {
            HealthController.SetDamage(damage);
        }
        
        private void ShootPlayer()
        {
            currentTimeToShoot += Program.DeltaTime;
            var direction = (Program.LevelScene.player.Transform.Position - Transform.Position).Normalize();
            if (currentTimeToShoot >= coolDownShoot)
            {
                Engine.Debug("shoot");
                shootController.Shoot(Transform.Position, direction);
                currentTimeToShoot = 0;
            }
        }
    }
}
