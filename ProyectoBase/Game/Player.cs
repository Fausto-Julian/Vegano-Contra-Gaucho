using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player : GameObject, IHealthController
    {
        public HealthController healthController { get; private set; }
        public float speed { get; set; }

        private bool aux;
        private float time;

        public Player(string id, float maxHealth, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, animation, startPosition, scale, angle)
        {
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;
        }

        public Player(string id, float maxHealth, float Speed, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, animation, startPosition, scale, angle)
        {
            speed = Speed;
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;
        }

        public override void Update()
        {
            if (Engine.GetKey(Keys.D))
            {
                if (Position.X <= Program.windowWidth - Animation.currentFrame.Height)
                {
                    var newX = Position.X + speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, Position.Y));
                }
            }

            if (Engine.GetKey(Keys.A))
            {
                if (Position.X >= 0)
                {
                    var newX = Position.X - speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, Position.Y));
                }
                    
            }

            if (Engine.GetKey(Keys.W))
            {
                if (Position.Y >= 0 + Animation.currentFrame.Width)
                {
                    var newY = Position.Y - speed * Program.deltaTime;

                    SetPosition(new Vector2(Position.X, newY));
                }
                
            }

            if (Engine.GetKey(Keys.S))
            {
                if (Position.Y <= Program.windowHeight)
                {
                    var newY = Position.Y + speed * Program.deltaTime;

                    SetPosition(new Vector2(Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.SPACE) && !aux)
            {
                GameObjectManager.AddGameObject(new Bullet($"Bullet{ID}", 100f, 20f,new Vector2(0, -1f), new Vector2(Position.X + Animation.currentFrame.Height / 2, Position.Y + (-Animation.currentFrame.Width - 50)), Animation));
                aux = true;
            }

            if (aux)
            {
                time += Program.deltaTime;

                if (time > 0.5)
                {
                    aux = false;
                    time = 0;
                }
            }

            base.Update();
        }

        public void GetDamage(float damage)
        {
            healthController.GetDamage(damage);
        }

        public void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);

            var aux = new List<Texture>();
            aux.Add(new Texture("playerIdleAnim_3.png"));

            Animation = new Animation(Animation.id, false, 0.2f, aux);
        }
    }
}
