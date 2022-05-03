using System;
using Game.Component;
using Game.Interface;

namespace Game.Objects
{
    public class Bullet : GameObject
    {
        private readonly string ownerId;
        private readonly float speed;
        private readonly float damage;
        private Vector2 direction;

        public Action OnDeactivate;

        public Bullet(string ownerId, float speed, float damage, Texture texture)
            :base($"Bullet{ownerId}", texture, Vector2.One, Vector2.One)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            BoxCollider.IsTrigger = true;
        }
        
        public Bullet(string ownerId, float speed, float damage, Animation animation)
            :base($"Bullet{ownerId}", animation, Vector2.One, Vector2.One)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            BoxCollider.IsTrigger = true;
        }

        public void InitializeBullet(Vector2 startPosition, Vector2 direction)
        {
            Transform.Position = startPosition;
            this.direction = direction;
        }

        public override void Update()
        {
            var newPos = Transform.Position + direction * speed * Program.DeltaTime;

            SetPosition(newPos);

            CheckCollision();

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

        private void CheckCollision()
        {
            if (BoxCollider.CheckCollision(out var collider, out var onTrigger, out var onCollision))
            {
                if (onTrigger)
                {
                    if (collider is IHealthController aux)
                    {
                        if (ownerId != collider.Id)
                        {
                            aux.SetDamage(damage);
                            OnDeactivate.Invoke();
                        }
                    }
                }
            }
        }
    }
}
