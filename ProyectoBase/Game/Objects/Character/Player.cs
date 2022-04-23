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

        private readonly ShootController shootController;

        public HealthController HealthController { get; private set; }
        private readonly float speed; 

        public Player(string id, float maxHealth, float speed, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            var animation = Animation.CreateAnimation("Texture/Player/Idle/PlayerAnimIdle_", 21, "Idle", true, 0.05f);

            Initialize(id, animation, startPosition, new Vector2(0.5f, 0.5f), angle);

            this.speed = speed;
            
            shootController = new ShootController(id, "Texture/Player/Bullet/BulletPlayer_", 200f, 20f, new Vector2(0, -1f), true);

            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += Destroy;

            GameManager.Instance.OnGamePause += GamePauseHandler;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.DeltaTime;

            BoxCollider.CheckCollision(out var collider, out var onTrigger, out var onCollision);

            var collitionRight = false;
            var collitionLeft = false;
            var collitionUp = false;
            var collitionDown = false;

            if (collider != null)
            {

                if (((Transform.Position.X + RealScale.X + 50) > collider.Transform.Position.X)
                    && (Transform.Position.X < collider.Transform.Position.X + collider.RealScale.X / 2) 
                    && onCollision)
                {
                    collitionRight = true;
                    Transform.Position.X -= 2.5f;
                }
                
                if ((Transform.Position.X - 50) < collider.Transform.Position.X + collider.RealScale.X 
                    && Transform.Position.X > collider.Transform.Position.X + collider.RealScale.X / 2
                    && onCollision)
                {
                    collitionLeft = true;
                    Transform.Position.X += 2.5f;
                }

                if ((Transform.Position.Y - 50) < collider.Transform.Position.Y + collider.RealScale.Y 
                    && Transform.Position.Y > collider.Transform.Position.Y + collider.RealScale.Y / 2
                    && onCollision)
                {
                    collitionUp = true;
                    Transform.Position.Y += 2.5f;
                }

                if ((Transform.Position.Y + RealScale.Y + 50) > collider.Transform.Position.Y 
                    && Transform.Position.Y < collider.Transform.Position.Y - collider.RealScale.Y / 2
                    && onCollision)
                {
                    collitionDown = true;
                    Transform.Position.Y -= 2.5f;
                }
            }

            
            

            if (Engine.GetKey(Keys.D) && !collitionRight)
            {
                if (Transform.Position.X + RealScale.X <= Program.WindowWidth)
                {
                    var newX = Transform.Position.X + speed * Program.DeltaTime;

                    SetPosition(new Vector2(newX, Transform.Position.Y));
                }
            }

            if (Engine.GetKey(Keys.A) && !collitionLeft)
            {
                if (Transform.Position.X >= 0)
                {
                    var newX = Transform.Position.X - speed * Program.DeltaTime;

                    SetPosition(new Vector2(newX, Transform.Position.Y));
                }
            }

            if (Engine.GetKey(Keys.W) && !collitionUp)
            {
                if (Transform.Position.Y >= 0)
                {
                    var newY = Transform.Position.Y - speed * Program.DeltaTime;

                    SetPosition(new Vector2(Transform.Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.S) && !collitionDown)
            {
                if (Transform.Position.Y + RealScale.Y <= Program.WindowHeight)
                {
                    var newY = Transform.Position.Y + speed * Program.DeltaTime;
                    SetPosition(new Vector2(Transform.Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.SPACE) && (currentInputDelayTime  > INPUT_DELAY) && !onCollision)
            {
                currentInputDelayTime = 0;
                var startPosition = new Vector2(Transform.Position.X - 50 + Animation.CurrentFrame.Width / 2, Transform.Position.Y + 45);
                shootController.Shoot(startPosition);
            }
            base.Update();
        }

        public void SetDamage(float damage)
        {
            HealthController.SetDamage(damage);
        }

        private void GamePauseHandler()
        {
            currentInputDelayTime = 0;
        }
    }
}
