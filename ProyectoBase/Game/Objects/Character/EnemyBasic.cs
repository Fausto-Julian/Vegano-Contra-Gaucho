using System;
using Game.Component;
using Game.Interface;

namespace Game.Objects.Character
{
    public class EnemyBasic : GameObject, IHealthController
    {
        private bool moveRight;
        
        private float currentTimeToShoot;
        private readonly float coolDownShoot;

        private readonly ShootController shootController;

        private HealthController HealthController { get; set; }

        public event Action OnDeactivate;
        
        public EnemyBasic(string id, Texture texture, float maxHealth, float coolDownShoot)
            : base(id, texture, Vector2.One, Vector2.One)
        {
            this.coolDownShoot = coolDownShoot;
            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += DeathHandler;
            shootController = new ShootController(id, new Texture("Texture/Player/Bullet/BulletPlayer_0.png"), 250f, 20f);
        }
        
        public void Initialize(Vector2 newPosition)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(HealthController.MaxHealth);
        }

        public override void Update()
        {
            ShootPlayer();
            if (Transform.Position.X + RealSize.X >= Program.WINDOW_WIDTH)
            {
                moveRight = false;
            }
            if (Transform.Position.X <= 0)
            {
                moveRight = true;
            }
            
            switch (moveRight)
            {
                case true:
                    Transform.Position.X += 200 * Program.DeltaTime;
                    break;
                case false:
                    Transform.Position.X -= 200 * Program.DeltaTime;
                    break;
            }
            base.Update();
        }
        
        private void DeathHandler()
        {
            OnDeactivate.Invoke();
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
                shootController.Shoot(Transform.Position, direction);
                currentTimeToShoot = 0;
            }
        }
    }
}
