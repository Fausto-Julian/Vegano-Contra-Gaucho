using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Boss : GameObject, IHealthController
    {
        private bool ChangeDirection = true;

        private int damageReduction = 1;

        private float CoolwdownChange = 0;

        private bool prueba = false;

        public HealthController healthController { get; private set; }
    
        public float speed { get; set; }

        public Boss(string BossID, float maxHealth, float Speed, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0) 
            : base(BossID, animation, startPosition, scale, angle)
        {
            speed = Speed;
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;
        }
        public override void Update()
        {
            BossMechanics();
            base.Update();
        }

        public void GetDamage(float damage)
        {
            healthController.GetDamage(damage / damageReduction);
        }

        // Mecanicas del boss
        // Todo: En el If hacer que se active cuando la vida llegue a < 51
        public void BossMechanics() 
        {
            BossMove();
            LifeLess();
            if (healthController.currentHealth <= healthController.maxHealth / 2)
            {
                LifeLess();
            }
        }
        public void BossMove() 
        {
            CoolwdownChange += Program.deltaTime;

            if (ChangeDirection)
            {
                var newDirection = transform.Position.x + speed * Program.deltaTime;
                SetPosition(new Vector2(newDirection, transform.Position.y));
            }
            if (!ChangeDirection)
            {
                var newDirection = transform.Position.x - speed * Program.deltaTime;
                SetPosition(new Vector2(newDirection, transform.Position.y));
            }

            if (transform.Position.x >= Program.windowWidth - animation.currentFrame.Height && CoolwdownChange >= 1)
            {
                ChangeDirection = false;
                CoolwdownChange = 0;
            }
            if (transform.Position.x <= 0 && CoolwdownChange >= 1)
            {
                ChangeDirection = true;
                CoolwdownChange = 0;
            }
        }
        public void LifeLess() 
        {
            Random Number = new Random();

            int RamdomActivate = Number.Next(1, 75);

            damageReduction = 2;

            if (RamdomActivate == 1 && transform.Position.x <= Program.windowWidth - animation.currentFrame.Height && transform.Position.x >= 0 
                && CoolwdownChange >= 1) 
            {
                if (transform.Position.x <= Program.windowWidth / 2)
                {
                    ChangeDirection = true;
                }
                else 
                {
                    ChangeDirection = false;
                }
            }
        }
    }
}
