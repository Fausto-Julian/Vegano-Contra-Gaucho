using System;
using Game.Components;
using Game.PhysicsEngine;

namespace Game.Objects
{
    public class Bullet : GameObject
    {
        private readonly string _ownerId;
        private readonly float _speed;
        private readonly float _damage;
        private Vector2 _direction;

        public Action OnDeactivate;

        public Bullet(string ownerId, float speed, float damage, Texture texture)
            :base($"Bullet{ownerId}", texture, Vector2.One, Vector2.One, TypeCollision.Circle, false, true)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;

            GetComponent<Body>().OnTrigger += OnTriggerHandler;
        }
        
        public Bullet(string ownerId, float speed, float damage, Animation animation)
            :base($"Bullet{ownerId}", animation, Vector2.One, Vector2.One, TypeCollision.Circle, true, false, true)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;

            GetComponent<Body>().OnTrigger += OnTriggerHandler;
        }

        public void InitializeBullet(Vector2 startPosition, Vector2 direction)
        {
            Transform.Position = startPosition;
            _direction = direction;
        }

        public override void Update()
        {
            var newPos = Transform.Position + _direction * _speed * Program.DeltaTime;

            Transform.Position = newPos;

            if (Transform.Position.Y + RealSize.Y <= 0)
            {
                OnDeactivate.Invoke();
            }
            else if (Transform.Position.Y >= Program.WINDOW_HEIGHT)
            {
                OnDeactivate.Invoke();
            }

            base.Update();
        }

        private void OnTriggerHandler(GameObject collision)
        {
            var healthController = collision.GetComponent<HealthController>();
            if (healthController != null)
            {
                if (_ownerId != collision.Id)
                {
                    healthController.SetDamage(_damage);
                    OnDeactivate.Invoke();
                }
            }
        }
    }
}
