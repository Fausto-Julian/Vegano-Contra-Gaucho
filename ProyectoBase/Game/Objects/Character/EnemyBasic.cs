using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Objects.Character
{
    public class EnemyBasic : GameObject, IHealthController
    {
        private bool movingright;
        public EnemyBasic(string id, Texture texture, Vector2 startPosition, float maxHealth)
            : base(id, texture, startPosition, Vector2.One, true)
        {
            HealthController = new HealthController(maxHealth);
        }


        private HealthController HealthController { get; set; }
        public void Initialize(Vector2 newPosition, float health)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(health);
        }

        public void SetDamage(float damage)
        {
            HealthController.SetDamage(damage);
        }

        public override void Update()
        {
            if (HealthController.CurrentHealth < HealthController.MaxHealth / 2)
            {
                Console.WriteLine("entro");
                Console.WriteLine(Transform.Position.X);
                //Console.WriteLine(movetimer);

                //movetimer += Program.deltaTime;
                if (Transform.Position.X >= Program.WindowWidth)
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
