using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player : GameObject, IHealthController
    {
        private const float INPUT_DELAY = 0.2f;
        private float currentInputDelayTime;

        public HealthController healthController { get; private set; }
        public float speed { get; set; }

        public Player(string id, float maxHealth, float Speed, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, animation, startPosition, scale, angle)
        {
            speed = Speed;
            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.deltaTime;
            if (Engine.GetKey(Keys.D))
            {
                if (transform.Position.X <= Program.windowWidth - Animation.currentFrame.Height)
                {
                    var newX = transform.Position.X + speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.Y));
                }
            }

            if (Engine.GetKey(Keys.A))
            {
                if (transform.Position.X >= 0)
                {
                    var newX = transform.Position.X - speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.Y));
                }

            }

            if (Engine.GetKey(Keys.W))
            {
                if (transform.Position.Y >= 0 + Animation.currentFrame.Width)
                {
                    var newY = transform.Position.Y - speed * Program.deltaTime;

                    SetPosition(new Vector2(transform.Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.S))
            {
                if (transform.Position.Y <= Program.windowHeight)
                {
                    var newY = transform.Position.Y + speed * Program.deltaTime;
                    SetPosition(new Vector2(transform.Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.SPACE) && (currentInputDelayTime  > INPUT_DELAY))
            {
                currentInputDelayTime = 0;
                new Bullet($"Bullet{ID}", 100f, 20f, new Vector2(0, -1f), new Vector2(transform.Position.X + Animation.currentFrame.Height / 2, transform.Position.Y + (-Animation.currentFrame.Width - 50)), Animation);
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

        private void GamePauseHandler()
        {
            currentInputDelayTime = 0;
        }
    }
}
