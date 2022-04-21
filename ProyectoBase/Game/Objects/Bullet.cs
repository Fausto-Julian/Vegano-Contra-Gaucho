using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Bullet : GameObject
    {
        private string ownerId;
        private float speed;
        private float damage;
        private Vector2 direction;

        public Action OnDesactivate;

        public Bullet()
        {
            boxCollider.isTrigger = true;
        }

        public void InitializeBullet(string OwnerId, float Speed, float Damage, Texture texture)
        {
            base.Initialize($"Bullet{ownerId}", texture, Vector2.One, Vector2.One);
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
        }

        public void InitializeBullet(string OwnerId, float Speed, float Damage, Animation animation)
        {
            base.Initialize($"Bullet{ownerId}", animation, Vector2.One, Vector2.One);
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
        }

        public void Trayectory(Vector2 startPosition, Vector2 direction)
        {
            transform.Position = startPosition;
            this.direction = direction;
        }

        public override void Update()
        {
            var newPos = transform.Position + direction * speed * Program.deltaTime;

            SetPosition(newPos);

            CheckCollision();

            if (IsAnimated)
            {
                if (transform.Position.y + animation.currentFrame.Height <= 0)
                {
                    OnDesactivate?.Invoke();
                }
            }
            else
            {
                if (transform.Position.y + texture.Height <= 0)
                {
                    OnDesactivate?.Invoke();
                }
            }
            

            base.Update();
        }

        private void CheckCollision()
        {
            //for (int i = 0; i < GameObjectManager.activeGameObjects.Count; i++)
            //{
            //    var gameObject = GameObjectManager.activeGameObjects[i];

            //    if (gameObject is IHealthController)
            //    {
            //        if (ownerId != gameObject.ID)
            //        {
            //            if (Collitions.BoxCollider(transform.Position, RealScale, gameObject.transform.Position, gameObject.RealScale))
            //            {
            //                var aux = (IHealthController)gameObject;
            //                aux.GetDamage(damage);
            //                OnDesactivate?.Invoke(this);
            //            }
            //        }
            //    }
            //}

            if (boxCollider.CheckCollision(out var collider, out var onTrigger, out var onCollision))
            {
                if (onTrigger)
                {
                    if (collider is IHealthController)
                    {
                        if (ownerId != collider.ID)
                        {
                            var aux = (IHealthController)collider;
                            aux.SetDamage(damage);
                            OnDesactivate?.Invoke();
                        }
                    }
                }
            }
        }
    }
}
