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
            BoxCollider.IsTrigger = true;
        }

        public void InitializeBullet(string ownerId, float speed, float damage, Texture texture)
        {
            base.Initialize($"Bullet{this.ownerId}", texture, Vector2.One, Vector2.One);
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
        }

        public void InitializeBullet(string ownerId, float speed, float damage, Animation animation)
        {
            base.Initialize($"Bullet{this.ownerId}", animation, Vector2.One, Vector2.One);
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
        }

        public void Trayectory(Vector2 startPosition, Vector2 direction)
        {
            Transform.Position = startPosition;
            this.direction = direction;
        }

        public override void Update()
        {
            var newPos = Transform.Position + direction * speed * Program.DeltaTime;

            SetPosition(newPos);

            CheckCollision();

            if (IsAnimated)
            {
                if (Transform.Position.Y + Animation.CurrentFrame.Height <= 0)
                {
                    OnDesactivate?.Invoke();
                }
            }
            else
            {
                if (Transform.Position.Y + Texture.Height <= 0)
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

            if (BoxCollider.CheckCollision(out var collider, out var onTrigger, out var onCollision))
            {
                if (onTrigger)
                {
                    if (collider is IHealthController aux)
                    {
                        if (ownerId != collider.Id)
                        {
                            aux.SetDamage(damage);
                            OnDesactivate?.Invoke();
                        }
                    }
                }
            }
        }
    }
}
