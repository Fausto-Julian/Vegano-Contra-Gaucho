﻿using System;
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

        public Action<Bullet> OnDesactivate;

        public Bullet()
        {
            
        }

        public void InitializeBullet(string OwnerId, float Speed, float Damage, Vector2 Direction, Vector2 StartPosition, Animation animation)
        {
            base.Initialize(OwnerId, animation, StartPosition, Vector2.One);
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            direction = Direction;
        }


        public override void Update()
        {
            var newPos = transform.Position + direction * speed * Program.deltaTime;

            SetPosition(newPos);

            CheckCollision();

            if (transform.Position.Y + Animation.currentFrame.Height <= 0)
            {
                OnDesactivate?.Invoke(this);
            }

            base.Update();
        }

        private void CheckCollision()
        {
            for (int i = 0; i < GameObjectManager.ActiveGameObjects.Count; i++)
            {
                var gameObject = GameObjectManager.ActiveGameObjects[i];

                if (gameObject is IHealthController)
                {
                    if (ownerId != gameObject.ID)
                    {
                        if (Collitions.BoxCollider(transform.Position, new Vector2(Animation.currentFrame.Width, Animation.currentFrame.Height), gameObject.transform.Position, new Vector2(gameObject.Animation.currentFrame.Width, gameObject.Animation.currentFrame.Height)))
                        {
                            var aux = (IHealthController)gameObject;
                            aux.GetDamage(damage);
                            OnDesactivate?.Invoke(this);
                        }
                    }
                }
            }
        }



    }
}