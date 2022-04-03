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
            
            Console.WriteLine(prueba);

            BossMechanics();
            base.Update();
        }
        public void GetDamage(float damage)
        {
            healthController.GetDamage(damage / damageReduction);
        }

        public void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);

            var aux = new List<Texture>();
            aux.Add(new Texture("playerIdleAnim_3.png"));

            Animation = new Animation(Animation.id, false, 0.2f, aux);
        }
        // Mecanicas del boss
        // Todo: En el If hacer que se active cuando la vida llegue a < 51
        public void BossMechanics() 
        {
            BossMove();
            LifeLess();
            //if (healthController.MaxHealth <= healthController.MaxHealth / 2) 
            //{
            //    prueba = true;
            //     LifeLess();
            //}
        }
        public void BossMove() 
        {
            CoolwdownChange += Program.deltaTime;

            if (ChangeDirection)
            {
                var newDirection = transform.Position.X + speed * Program.deltaTime;
                SetPosition(new Vector2(newDirection, transform.Position.Y));
            }
            if (!ChangeDirection)
            {
                var newDirection = transform.Position.X - speed * Program.deltaTime;
                SetPosition(new Vector2(newDirection, transform.Position.Y));
            }

            if (transform.Position.X >= Program.windowWidth - Animation.currentFrame.Height && CoolwdownChange >= 1)
            {
                ChangeDirection = false;
                CoolwdownChange = 0;
            }
            if (transform.Position.X <= 0 && CoolwdownChange >= 1)
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

            if (RamdomActivate == 1 && transform.Position.X <= Program.windowWidth - Animation.currentFrame.Height && transform.Position.X >= 0 
                && CoolwdownChange >= 1) 
            {
                if (transform.Position.X <= Program.windowWidth / 2)
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
