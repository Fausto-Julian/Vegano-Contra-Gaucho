using System;
using Game.Component;
using Game.Interface;

namespace Game.Objects.Character
{
    public class Boss : GameObject, IHealthController
    {
        public HealthController HealthController { get; private set; }
        
        private bool changeDirection = true;
        private int damageReduction = 1;
        private float coolDownChange = 0;
        private float coolDownShoot;
        private float currentTimingShoot;
        private float speed;

        private ShootController shootController;

        private Transform playerTranform;

        private LifeBar lifeBar;

        public Boss(string bossId, float maxHealth, float speed, float coolDownShoot, Texture texture, Vector2 startPosition) 
            : base(bossId, texture, startPosition, Vector2.One)
        {
            this.speed = speed;
            this.coolDownShoot = coolDownShoot;
            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += Destroy;
            lifeBar = new LifeBar($"lifeBar{bossId}", HealthController, new Texture("Texture/LineBackground.png"), new Texture("Texture/Line.png"), new Vector2(50f, 50f));
            shootController = new ShootController(bossId, new Texture("Texture/Lettuce.png"), 250f, 20f);
            playerTranform = GameObjectManager.FindWithTag("Player").Transform;
        }
        
        public override void Update()
        {
            BossMechanics();
            base.Update();
        }
        
        public void SetDamage(float damage)
        {
            HealthController.SetDamage(damage / damageReduction);
        }

        private void BossMechanics() 
        {
            BossMove();
            if (HealthController.CurrentHealth <= HealthController.MaxHealth / 2)
            {
                LifeLess(2, 475f);
            }
            else if (HealthController.CurrentHealth <= HealthController.MaxHealth / 4) 
            {
                LifeLess(3, 500f);
            }
            ShootPlayer();
        }

        private void BossMove() 
        {
            coolDownChange += Program.DeltaTime;

            switch (changeDirection)
            {
                case true:
                {
                    var newDirection = Transform.Position.X + speed * Program.DeltaTime;
                    SetPosition(new Vector2(newDirection, Transform.Position.Y));
                    break;
                }
                case false:
                {
                    var newDirection = Transform.Position.X - speed * Program.DeltaTime;
                    SetPosition(new Vector2(newDirection, Transform.Position.Y));
                    break;
                }
            }

            if (Transform.Position.X >= Program.WINDOW_WIDTH - RealSize.X && coolDownChange >= 1)
            {
                changeDirection = false;
                coolDownChange = 0;
            }
            if (Transform.Position.X <= 0 && coolDownChange >= 1)
            {
                changeDirection = true;
                coolDownChange = 0;
            }
        }

        private void LifeLess(int reduction, float speed) 
        {
            var number = new Random();

            var randomActivate = number.Next(1, 75);

            damageReduction = reduction;

            this.speed = speed;

            if (randomActivate == 1 && Transform.Position.X <= Program.WINDOW_WIDTH - RealSize.X && Transform.Position.X >= 0 
                && coolDownChange >= 1) 
            {
                if (Transform.Position.X <= Program.WINDOW_WIDTH / 2)
                {
                    changeDirection = true;
                    coolDownChange = 0;
                }
                else 
                {
                    changeDirection = false;
                    coolDownChange = 0;
                }
            }
        }

        private void ShootPlayer()
        {
            currentTimingShoot += Program.DeltaTime;
            if (playerTranform != null)
            {
                var direction = (playerTranform.Position - Transform.Position).Normalize();

                if (currentTimingShoot >= coolDownShoot)
                {
                    currentTimingShoot = 0;
                    shootController.Shoot(Transform.Position, direction);
                }  
            }
        }
    }
}
