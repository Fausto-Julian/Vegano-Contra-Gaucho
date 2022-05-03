using System;
using Game.Component;
using Game.Interface;

namespace Game.Objects.Character
{
    public class EnemyBasic : GameObject, IHealthController
    {
        private bool moveRight;
        
        private float currentTimeToShoot;
        private float coolDownShoot;
        private Transform playerTranform;

        private ShootController shootController;

        private HealthController healthController;

        public event Action OnDeactivate;
        
        public EnemyBasic(string id, Texture texture, float maxHealth, float coolDownShoot)
            : base(id, texture, Vector2.One, Vector2.One)
        {
            this.coolDownShoot = coolDownShoot;
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += OnDeathHandler;
            shootController = new ShootController(id, new Texture("Texture/molly.png"), 250f, 20f);
        }
        
        public void Initialize(Vector2 newPosition)
        {
            Transform.Position = newPosition;
            healthController.SetHealth(healthController.MaxHealth);
            playerTranform = GameObjectManager.FindWithTag("Player").Transform;
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
        
        private void OnDeathHandler()
        {
            OnDeactivate.Invoke();
        }
        
        public void SetDamage(float damage)
        {
            healthController.SetDamage(damage);
        }
        
        private void ShootPlayer()
        {
            currentTimeToShoot += Program.DeltaTime;
            if (playerTranform != null)
            {
                var direction = (playerTranform.Position - Transform.Position).Normalize();
                if (currentTimeToShoot >= coolDownShoot)
                {
                    shootController.Shoot(Transform.Position, direction);
                    currentTimeToShoot = 0;
                }
            }
        }
    }
}
