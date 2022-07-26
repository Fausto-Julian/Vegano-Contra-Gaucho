using System;
using Game.Components;
using Game.PhysicsEngine;

namespace Game.Objects.Character
{
    public class Player : GameObject
    {
        private readonly ShootController _shootController;

        private readonly float _speed;
        private LifeBar _lifeBar;

        private readonly AnimationController _animationController;

        private readonly int aux;

        public Player(string id, float maxHealth, float speed, Vector2 startPosition, Vector2 scale, float angle = 0)
            : base(id, Animation.CreateAnimation("Idle", "Texture/Player/Idle/PlayerAnimIdle_", 21, true, 0.05f), startPosition, Vector2.One, TypeCollision.Box, true)
        {
            _speed = speed;
            
            // Animations and shoot
            _animationController = new AnimationController(this);
            if (GameManager.Instance.ModeVegan)
            {
                _animationController.AddAnimation(Animation.CreateAnimation("Idle", "Texture/Player/VeganPlayer_", 2, false, 0.2f));
                
                _shootController = new ShootController(this, id, new Texture("Texture/Enemies/molly.png"), 200f, 20f, new Vector2(0, -1f));

                Transform.Scale = new Vector2(0.5f, 0.5f);
            }
            else
            {
                _shootController = new ShootController(this, id,"Texture/Player/Bullet/BulletPlayer_", 200f, 20f, new Vector2(0, -1f));
                _animationController.AddAnimation(Animation.CreateAnimation("Idle", "Texture/Player/Idle/PlayerAnimIdle_", 21, true, 0.05f));
            }
            _animationController.ChangeAnimation("Idle");

            // Life
            var healthController = new HealthController(this, maxHealth);
            healthController.OnDeath += Destroy;

            
            Components.Add(_animationController);
            Components.Add(_shootController);
            Components.Add(healthController);
            
            _lifeBar = new LifeBar(id, new Vector2(25f, 1020f));
            var random = new Random();
            aux = random.Next(0, 100);
        }

        public override void Update()
        {
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

            if (!Input.GetKeyStay(Keys.D) && !Input.GetKeyStay(Keys.A))
            {
                if (Renderer.Animation.Id != "Idle")
                {
                    _animationController.ChangeAnimation("Idle");
                }
            }
            
            if (Input.GetKeyDown(Keys.SPACE))
            {
                var x = Transform.Position.X;
                var y = Transform.Position.Y;
                var startPosition = new Vector2(x - 50 + RealSize.X / 2, y + 45);
                _shootController.Shoot(startPosition);
            }
            base.Update();
        }
    }
}
