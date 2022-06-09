using System;
using Game.Components;
using Game.PhysicsEngine;

namespace Game.Objects.Character
{
    public class Boss : GameObject
    {
        private bool _changeDirection = true;

        private int _damageReduction = 1;

        private float _coolDownChange = 0;

        private readonly float _coolDownShoot;

        private float _currentTimingShoot;

        private readonly ShootController _shootController;

        private readonly Transform _playerTransform;

        private LifeBar _lifeBar;
        
        private readonly HealthController _healthController;
        private float Speed { get; set; }

        public Boss(string bossId, float maxHealth, float speed, float coolDownShoot, Texture texture, Vector2 startPosition) 
            : base(bossId, texture, startPosition, Vector2.One, TypeCollision.Box, true)
        {
            Speed = speed;
            _coolDownShoot = coolDownShoot;
            _healthController = new HealthController(this, maxHealth);
            _healthController.OnDeath += Destroy;
            
            Components.Add(_healthController);
            
            _lifeBar = new LifeBar($"lifeBar{bossId}", new Texture("Texture/LineBackground.png"), new Texture("Texture/Line.png"), new Vector2(50f, 50f));
            _shootController = new ShootController(this, bossId, new Texture("Texture/Lettuce.png"), 250f, 20f);
            
            Components.Add(_shootController);
            
            _playerTransform = GameObjectManager.FindWithTag("Player").Transform;
        }
        
        public override void Update()
        {
            BossMechanics();
            base.Update();
        }
        
        public void SetDamage(float damage)
        {
            _healthController.SetDamage(damage / _damageReduction);
        }

        private void BossMechanics() 
        {
            BossMove();
            if (_healthController.CurrentHealth <= _healthController.MaxHealth / 2)
            {
                LifeLess(2, 475f);
            }
            else if (_healthController.CurrentHealth <= _healthController.MaxHealth / 4) 
            {
                LifeLess(3, 500f);
            }
            ShootPlayer();
        }

        private void BossMove() 
        {
            _coolDownChange += Program.DeltaTime;

            switch (_changeDirection)
            {
                case true:
                {
                    var newDirection = Transform.Position.X + Speed * Program.DeltaTime;
                    Transform.Position = new Vector2(newDirection, Transform.Position.Y);
                    break;
                }
                case false:
                {
                    var newDirection = Transform.Position.X - Speed * Program.DeltaTime;
                    Transform.Position = new Vector2(newDirection, Transform.Position.Y);
                    break;
                }
            }

            if (Transform.Position.X >= Program.WINDOW_WIDTH - RealSize.X && _coolDownChange >= 1)
            {
                _changeDirection = false;
                _coolDownChange = 0;
            }
            if (Transform.Position.X <= 0 && _coolDownChange >= 1)
            {
                _changeDirection = true;
                _coolDownChange = 0;
            }
        }

        private void LifeLess(int reduction, float speed) 
        {
            var number = new Random();

            var randomActivate = number.Next(1, 75);

            _damageReduction = reduction;

            Speed = speed;

            if (randomActivate == 1 && Transform.Position.X <= Program.WINDOW_WIDTH - RealSize.X && Transform.Position.X >= 0 
                && _coolDownChange >= 1) 
            {
                if (Transform.Position.X <= Program.WINDOW_WIDTH / 2)
                {
                    _changeDirection = true;
                    _coolDownChange = 0;
                }
                else 
                {
                    _changeDirection = false;
                    _coolDownChange = 0;
                }
            }
        }

        private void ShootPlayer()
        {
            _currentTimingShoot += Program.DeltaTime;
            if (_playerTransform != null)
            {
                var direction = (_playerTransform.Position - Transform.Position).Normalized;

                if (_currentTimingShoot >= _coolDownShoot)
                {
                    _currentTimingShoot = 0;
                    _shootController.Shoot(Transform.Position, direction);
                }  
            }
        }
    }
}
