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
            var animation = Animation.CreateAnimation("Texture/Player/Idle/PlayerAnimIdle_", 21, "Idle", true, 0.05f);

            Initialize(id, animation, startPosition, new Vector2(0.5f, 0.5f), angle);

            speed = Speed;
            
            shootController = new ShootController(id, "Texture/Player/Bullet/BulletPlayer_", 200f, 20f, new Vector2(0, -1f), true);

            healthController = new HealthController(maxHealth);
            healthController.OnDeath += Destroy;

            GameManager.Instance.OnGamePause += GamePauseHandler;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.deltaTime;
            var onCollision = false;

            boxCollider.CheckCollision(out var collider, out var onTrigger, out onCollision);

            bool collitionRight = false;
            bool collitionLeft = false;
            bool collitionUp = false;
            bool collitionDown = false;

            if (collider != null)
            {

                if (((transform.Position.x + RealScale.x + 50) > collider.transform.Position.x)
                    && (transform.Position.x < collider.transform.Position.x + collider.RealScale.x / 2) 
                    && onCollision)
                {
                    collitionRight = true;
                    transform.Position.x -= 2.5f;
                }
                
                if ((transform.Position.x - 50) < collider.transform.Position.x + collider.RealScale.x 
                    && transform.Position.x > collider.transform.Position.x + collider.RealScale.x / 2
                    && onCollision)
                {
                    collitionLeft = true;
                    transform.Position.x += 2.5f;
                }

                if ((transform.Position.y - 50) < collider.transform.Position.y + collider.RealScale.y 
                    && transform.Position.y > collider.transform.Position.y + collider.RealScale.y / 2
                    && onCollision)
                {
                    collitionUp = true;
                    transform.Position.y += 2.5f;
                }

                if ((transform.Position.y + RealScale.y + 50) > collider.transform.Position.y 
                    && transform.Position.y < collider.transform.Position.y - collider.RealScale.y / 2
                    && onCollision)
                {
                    collitionDown = true;
                    transform.Position.y -= 2.5f;
                }
            }

            
            

            if (Engine.GetKey(Keys.D) && !collitionRight)
            {
                if (transform.Position.x + texture.Width <= Program.windowWidth)
                {
                    var newX = transform.Position.x + speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.y));
                }
            }

            if (Engine.GetKey(Keys.A) && !collitionLeft)
            {
                if (transform.Position.x >= 0)
                {
                    var newX = transform.Position.x - speed * Program.deltaTime;

                    SetPosition(new Vector2(newX, transform.Position.y));
                }
            }

            if (Engine.GetKey(Keys.W) && !collitionUp)
            {
                if (transform.Position.y >= 0)
                {
                    var newY = transform.Position.y - speed * Program.deltaTime;

                    SetPosition(new Vector2(transform.Position.x, newY));
                }
            }

            if (Engine.GetKey(Keys.S) && !collitionDown)
            {
                if (transform.Position.y + texture.Height <= Program.windowHeight)
                {
                    var newY = transform.Position.y + speed * Program.deltaTime;
                    SetPosition(new Vector2(transform.Position.x, newY));
                }
            }

            if (Engine.GetKey(Keys.SPACE) && (currentInputDelayTime  > INPUT_DELAY) && !onCollision)
            {
                currentInputDelayTime = 0;
                var startPosition = new Vector2(transform.Position.x - 50 + animation.currentFrame.Width / 2, transform.Position.y + 45);
                shootController.Shoot(startPosition);
            }
            base.Update();
        }

        public void SetDamage(float damage)
        {
            healthController.SetDamage(damage);
        }

        private void GamePauseHandler()
        {
            currentInputDelayTime = 0;
        }
    }
}
