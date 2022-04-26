using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Boss : GameObject, IHealthController
    {
        private bool changeDirection = true;

        private int damageReduction = 1;

        private float coolDownChange = 0;

        private readonly float coolDownShoot;

        private float currentTimingShoot;

        private readonly ShootController shootController;

        public HealthController HealthController { get; private set; }

        private LifeBar lifeBar;

        private float Speed { get; set; }

        public Boss(string bossId, float maxHealth, float speed, float coolDownShoot, Texture texture, Vector2 startPosition) 
            : base(bossId, texture, startPosition, Vector2.One)
        {
            this.Speed = speed;
            this.coolDownShoot = coolDownShoot;
            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += Destroy;
            lifeBar = new LifeBar(bossId, HealthController, new Texture("Texture/LineBackground.png"), new Texture("Texture/Line.png"), new Vector2(100f, 100f));
            shootController = new ShootController(bossId, "Texture/Line.png", 250f, 20f, false);
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

        // Mecanicas del boss
        private void BossMechanics() 
        {
            BossMove();
            if (HealthController.CurrentHealth <= HealthController.MaxHealth / 2)
            {
                LifeLess(2, 475f);
            }
            else if (HealthController.CurrentHealth <= HealthController.MaxHealth / 4) 
            {
                // agregado
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
                    var newDirection = Transform.Position.X + Speed * Program.DeltaTime;
                    SetPosition(new Vector2(newDirection, Transform.Position.Y));
                    break;
                }
                case false:
                {
                    var newDirection = Transform.Position.X - Speed * Program.DeltaTime;
                    SetPosition(new Vector2(newDirection, Transform.Position.Y));
                    break;
                }
            }

            if (Transform.Position.X >= Program.WindowWidth - RealScale.X && coolDownChange >= 1)
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

            var ramdomActivate = number.Next(1, 75);

            damageReduction = reduction;

            this.Speed = speed;

            if (ramdomActivate == 1 && Transform.Position.X <= Program.WindowWidth - RealScale.X && Transform.Position.X >= 0 
                && coolDownChange >= 1) 
            {
                if (Transform.Position.X <= Program.WindowWidth / 2)
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
            var direction = (Program.LevelScene2.Player.Transform.Position - Transform.Position).Normalize();

            if (currentTimingShoot >= coolDownShoot)
            {
                currentTimingShoot = 0;
                shootController.Shoot(Transform.Position, direction);
            }
        }
    }
}
