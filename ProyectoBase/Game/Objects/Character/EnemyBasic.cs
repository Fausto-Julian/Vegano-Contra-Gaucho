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
        
        public EnemyBasic(string id, Texture texture, float maxHealth, float coolDownShoot)
            : base(id, texture, Vector2.One, Vector2.One, TypeCollision.Box, true)
        {
            _coolDownShoot = coolDownShoot;
            HealthController = new HealthController(this, maxHealth);
            HealthController.OnDeath += DeathHandler;
            Components.Add(HealthController);
            _shootController = new ShootController(this, id, new Texture("Texture/molly.png"), 250f, 20f);
            Components.Add(_shootController);
        }
        
        public void Initialize(Vector2 newPosition)
        {
            Transform.Position = newPosition;
            HealthController.SetHealth(HealthController.MaxHealth);
            _playerTransform = GameObjectManager.FindWithTag("Player").Transform;
        }

        public override void Update()
        {
            ShootPlayer();
            if (Transform.Position.X + RealSize.X >= Program.WINDOW_WIDTH)
            {
                _moveRight = false;
            }
            if (Transform.Position.X <= 0)
            {
                _moveRight = true;
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
