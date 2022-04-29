using Game.Component;
using Game.Interface;

namespace Game.Objects.Character
{
    public class Player : GameObject, IHealthController
    {
        private const float INPUT_DELAY = 0.2f;
        private float currentInputDelayTime;

        private readonly ShootController shootController;

        public HealthController HealthController { get; private set; }
        private readonly float speed; 

        public Player(string id, float maxHealth, float speed, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, Animation.CreateAnimation("Texture/Player/Idle/PlayerAnimIdle_", 21, true, 0.05f), startPosition, Vector2.One)
        {
            this.speed = speed;
            
            shootController = new ShootController(id,"Texture/Player/Bullet/BulletPlayer_", 200f, 20f, new Vector2(0, -1f));

            HealthController = new HealthController(maxHealth);
            HealthController.OnDeath += Destroy;

            GameManager.Instance.OnGamePause += GamePauseHandler;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.DeltaTime;

            BoxCollider.CheckCollision(out var collider, out var onTrigger, out var onCollision);

            var collisionRight = false;
            var collisionLeft = false;
            
            //var collisionUp = false;
            //var collisionDown = false;

            if (collider != null)
            {

                if (((Transform.Position.X + RealSize.X + 50) > collider.Transform.Position.X)
                    && (Transform.Position.X < collider.Transform.Position.X + collider.RealSize.X / 2) 
                    && onCollision)
                {
                    collisionRight = true;
                    Transform.Position.X -= 2.5f;
                }
                
                if ((Transform.Position.X - 50) < collider.Transform.Position.X + collider.RealSize.X 
                    && Transform.Position.X > collider.Transform.Position.X + collider.RealSize.X / 2
                    && onCollision)
                {
                    collisionLeft = true;
                    Transform.Position.X += 2.5f;
                }
                /*
                if ((Transform.Position.Y - 50) < collider.Transform.Position.Y + collider.RealSize.Y 
                    && Transform.Position.Y > collider.Transform.Position.Y + collider.RealSize.Y / 2
                    && onCollision)
                {
                    collisionUp = true;
                    Transform.Position.Y += 2.5f;
                }

                if ((Transform.Position.Y + RealSize.Y + 50) > collider.Transform.Position.Y 
                    && Transform.Position.Y < collider.Transform.Position.Y - collider.RealSize.Y / 2
                    && onCollision)
                {
                    collisionDown = true;
                    Transform.Position.Y -= 2.5f;
                }
                */
            }

            if (Engine.GetKey(Keys.D) && !collisionRight)
            {
                if (Transform.Position.X + RealSize.X <= Program.WINDOW_WIDTH)
                {
                    var newX = Transform.Position.X + speed * Program.DeltaTime;

                    SetPosition(new Vector2(newX, Transform.Position.Y));
                }
            }

            if (Engine.GetKey(Keys.A) && !collisionLeft)
            {
                if (Transform.Position.X >= 0)
                {
                    var newX = Transform.Position.X - speed * Program.DeltaTime;

                    SetPosition(new Vector2(newX, Transform.Position.Y));
                }
            }
            /*
            if (Engine.GetKey(Keys.W) && !collisionUp)
            {
                if (Transform.Position.Y >= 0)
                {
                    var newY = Transform.Position.Y - speed * Program.DeltaTime;

                    SetPosition(new Vector2(Transform.Position.X, newY));
                }
            }

            if (Engine.GetKey(Keys.S) && !collisionDown)
            {
                if (Transform.Position.Y + RealScale.Y <= Program.WINDOW_HEIGHT)
                {
                    var newY = Transform.Position.Y + speed * Program.DeltaTime;
                    SetPosition(new Vector2(Transform.Position.X, newY));
                }
            }
            */
            if (Engine.GetKey(Keys.SPACE) && (currentInputDelayTime  > INPUT_DELAY) && !onCollision)
            {
                currentInputDelayTime = 0;
                var startPosition = new Vector2(Transform.Position.X - 50 + RealSize.X / 2, Transform.Position.Y + 45);
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
