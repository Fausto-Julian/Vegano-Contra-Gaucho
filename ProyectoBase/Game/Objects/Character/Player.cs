using Game.Components;
using Game.PhysicsEngine;

namespace Game.Objects.Character
{
    public class Player : GameObject
    {
        private const float INPUT_DELAY = 0.5f;
        private float _currentInputDelayTime;

        private readonly ShootController _shootController;

        private readonly float _speed;
        private LifeBar _lifeBar;

        public Player(string id, float maxHealth, float speed, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, Animation.CreateAnimation("Texture/Player/Idle/PlayerAnimIdle_", 21, true, 0.05f), startPosition, Vector2.One, TypeCollision.Box, true)
        {
            _speed = speed;
            
            _shootController = new ShootController(this, id,"Texture/Player/Bullet/BulletPlayer_", 200f, 20f, new Vector2(0, -1f));
            
            Components.Add(_shootController);

            var healthController = new HealthController(this, maxHealth);
            healthController.OnDeath += Destroy;
            
            Components.Add(healthController);
            
            _lifeBar = new LifeBar(id, new Texture("Texture/LineBackground.png"), new Texture("Texture/Line.png"), new Vector2(50f, 1000f));
            
            GameManager.Instance.OnGamePause += OnGamePauseHandler;
        }

        public override void Update()
        {
            _currentInputDelayTime += Program.DeltaTime;

            if (Input.GetKeyStay(Keys.D))
            {
                if (Transform.Position.X + RealSize.X <= Program.WINDOW_WIDTH)
                {
                    var newX = Transform.Position.X + _speed * Program.DeltaTime;

                    Transform.Position = new Vector2(newX, Transform.Position.Y);
                }
            }

            if (Input.GetKeyStay(Keys.A))
            {
                if (Transform.Position.X >= 0)
                {
                    var newX = Transform.Position.X - _speed * Program.DeltaTime;

                    Transform.Position = new Vector2(newX, Transform.Position.Y);
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
            if (Engine.GetKey(Keys.SPACE) && (_currentInputDelayTime  > INPUT_DELAY))
            {
                _currentInputDelayTime = 0;
                var startPosition = new Vector2(Transform.Position.X - 50 + RealSize.X / 2, Transform.Position.Y + 45);
                _shootController.Shoot(startPosition);
            }
            base.Update();
        }

        private void OnGamePauseHandler()
        {
            _currentInputDelayTime = 0;
        }
    }
}
