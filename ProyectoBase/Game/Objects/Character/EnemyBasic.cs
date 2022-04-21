using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Objects.Character
{
    public class EnemyBasic : GameObject, IHealthController
    {
        float movetimer;
        private bool movingright;
        public EnemyBasic(string id, Texture texture, Vector2 startPosition, float maxHealth)
            : base(id, texture, startPosition, Vector2.One, true)
        {
            healthController = new HealthController(maxHealth);
        }


        public HealthController healthController { get; private set; }
        public void Initialize(Vector2 newPosition, float health)
        {
            transform.Position = newPosition;
            healthController.SetHealth(health);
        }

        public void SetDamage(float damage)
        {
            healthController.SetDamage(damage);
        }

        public override void Update()
        {
            if (healthController.currentHealth < healthController.maxHealth / 2)
            {
                Console.WriteLine("entro");
                Console.WriteLine(transform.Position.x);
                //Console.WriteLine(movetimer);

                //movetimer += Program.deltaTime;
                if (transform.Position.x >= Program.windowWidth)
                {
                    movingright = false;
                }
                if (transform.Position.x <= 0)
                {
                    movingright = true;
                }
                if (movingright)
                {
                    transform.Position.x += 2;
                }
                if (movingright == false)
                {
                    transform.Position.x -= 2;
                }

                /*if (movetimer > 30)
                {
                    movetimer = 0;
                }
               */
            }
            base.Update();
        }
    }
}
