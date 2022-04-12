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

        private ShootController shootController;

        public HealthController healthController { get; private set; }
        public float speed { get; set; }

        public Player(string id, float maxHealth, float Speed, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            var animation = Animation.CreateAnimation("Texture/Player/Idle/PlayerAnimIdle_", 21, "Idle", true, 0.2f);

            Initialize(id, animation, startPosition, scale, angle);

            speed = Speed;
            
            var bulletAnim = Animation.CreateAnimation("Texture/Player/Bullet/BulletPlayer_", 21, "Idle", true, 0.2f);
            shootController = new ShootController(id, 100f, 20f, new Vector2(0, -1f), bulletAnim);

            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;

            GameManager.Instance.OnGamePause += GamePauseHandler;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.deltaTime;
            if (Engine.GetKey(Keys.D))
            {
                if (transform.Position.x <= Program.windowWidth - animation.currentFrame.Height)
                {
                    var newX = transform.Position.x + speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.y));
                }
            }

            if (Engine.GetKey(Keys.A))
            {
                if (transform.Position.x >= 0)
                {
                    var newX = transform.Position.x - speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.y));
                }
            }

            if (Engine.GetKey(Keys.W))
            {
                if (transform.Position.y >= 0 + animation.currentFrame.Width)
                {
                    var newY = transform.Position.y - speed * Program.deltaTime;

                    SetPosition(new Vector2(transform.Position.x, newY));
                }
            }

            if (Engine.GetKey(Keys.S))
            {
                if (transform.Position.y <= Program.windowHeight)
                {
                    var newY = transform.Position.y + speed * Program.deltaTime;
                    SetPosition(new Vector2(transform.Position.x, newY));
                }
            }

            if (Engine.GetKey(Keys.SPACE) && (currentInputDelayTime  > INPUT_DELAY))
            {
                currentInputDelayTime = 0;
                shootController.Shoot(new Vector2(transform.Position.x + animation.currentFrame.Width / 2, transform.Position.y));
            }
            base.Update();
        }

        public void GetDamage(float damage)
        {
            healthController.GetDamage(damage);
        }

        private void GamePauseHandler()
        {
            currentInputDelayTime = 0;
        }
    }
}
