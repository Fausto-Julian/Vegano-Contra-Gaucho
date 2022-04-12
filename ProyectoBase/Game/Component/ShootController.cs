using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ShootController
    {
        private PoolGeneric<Bullet> bulletsPool = new PoolGeneric<Bullet>();

        private string ownerId;
        private float speed;
        private float damage;
        private Vector2 direction;

        private bool isAnimation;

        private Texture texture;
        private Animation animation;

        public ShootController(string OwnerId, float Speed, float Damage, Vector2 Direction, Texture texture)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            direction = Direction;
            isAnimation = false;
            this.texture = texture;
        }

        public ShootController(string OwnerId, float Speed, float Damage, Texture texture)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            isAnimation = false;
            this.texture = texture;
        }

        public ShootController(string OwnerId, float Speed, float Damage, Vector2 Direction, Animation animation)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            direction = Direction;
            isAnimation = true;
            this.animation = animation;
        }

        public ShootController(string OwnerId, float Speed, float Damage, Animation animation)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            isAnimation = true;
            this.animation = animation;
        }

        public void Shoot(Vector2 StartPosition)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                if (isAnimation)
                {
                    bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, animation);
                }
                else
                {
                    bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, texture);
                }
            }
        }

        public void Shoot(Vector2 StartPosition, Vector2 direction)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                if (isAnimation)
                {
                    bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, animation);
                }
                else
                {
                    bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, texture);
                }
            }
        }

        private Bullet CreateBullet()
        {
            var bullet = bulletsPool.GetorCreate();

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet();
                bullet.Value.OnDesactivate += (Bullet bull) =>
                {
                    if (bulletsPool.AvailablesCount > 15)
                    {
                        bull.Destroy();
                    }
                    else
                    {
                        bull.SetActive(false);
                        bulletsPool.AddPool(bullet);
                    }
                };
            }
            return bullet.Value;
        }
    }
}
