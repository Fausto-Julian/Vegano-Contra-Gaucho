using System;
using Game.Components;
using Game.PhysicsEngine;

namespace Game.Objects.Character
{
    public class EnemyBasic : GameObject
    {
        private bool _moveRight;
        
        private float _currentTimeToShoot;
        private readonly float _coolDownShoot;
        private Transform _playerTransform;

        private readonly ShootController _shootController;
        private HealthController HealthController { get; }

        public event Action OnDeactivate;
        public event Action<EnemyBasic> OnDeath;

        private readonly AnimationController _animationController;
        
        public EnemyBasic(string id, Animation rightAnimation, Animation leftAnimation, Texture textureBullet, float maxHealth, float coolDownShoot)
            : base(id, rightAnimation, Vector2.One, Vector2.One, TypeCollision.Box, true)
        {
            _coolDownShoot = coolDownShoot;
            
            // Animations
            _animationController = new AnimationController(this);
            _animationController.AddAnimation(rightAnimation);
            _animationController.AddAnimation(leftAnimation);
            
            // Life
            HealthController = new HealthController(this, maxHealth);
            HealthController.OnDeath += DeathHandler;
            
            // Shoots
            _shootController = new ShootController(this, id, textureBullet, 250f, 20f);
            
            // Add Components
            Components.Add(_animationController);
            Components.Add(HealthController);
            Components.Add(_shootController);
        }
        
        public void Initialize(Vector2 newPosition)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(HealthController.MaxHealth);
            _playerTransform = GameObjectManager.FindWithTag("Player").Transform;

            _animationController.ChangeAnimation(newPosition.X < 960 ? "Right" : "Left");
        }

        public override void Update()
        {
            ShootPlayer();
            if (Transform.Position.X + RealSize.X >= Program.WINDOW_WIDTH)
            {
                _moveRight = false;
                _animationController.ChangeAnimation("Left");
            }
            
            if (Transform.Position.X <= 0)
            {
                _moveRight = true;
                _animationController.ChangeAnimation("Right");
            }
            
            switch (_moveRight)
            {
                case true:
                    Transform.Position.X += 200 * Program.DeltaTime;
                    break;
                case false:
                    Transform.Position.X -= 200 * Program.DeltaTime;
                    break;
            }
            base.Update();
        }
        
        private void DeathHandler()
        {
            OnDeactivate.Invoke();
            OnDeath?.Invoke(this);
        }
        
        private void ShootPlayer()
        {
            _currentTimeToShoot += Program.DeltaTime;
            if (_playerTransform != null)
            {
                var direction = (_playerTransform.Position - Transform.Position).Normalized;
                if (_currentTimeToShoot >= _coolDownShoot)
                {
                    _shootController.Shoot(Transform.Position, direction);
                    _currentTimeToShoot = 0;
                }
            }
        }
    }
}
