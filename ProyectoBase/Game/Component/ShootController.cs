using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ShootController
    {
        private BulletPool bulletPool;

        private string ownerId;
        private float speed;
        private float damage;
        private Vector2 direction;
        private Animation animation;

        public ShootController(string OwnerId, float Speed, float Damage, Vector2 Direction, Animation animation)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            direction = Direction;
            this.animation = animation;

            bulletPool = new BulletPool();
        }

        public ShootController(string OwnerId, float Speed, float Damage, Animation animation)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            this.animation = animation;

            bulletPool = new BulletPool();
        }

        public void Shoot(Vector2 StartPosition)
        {
            Bullet bullet = bulletPool.GetBullet();

            if (bullet != null)
            {
                bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, animation);
            }
        }

        public void Shoot(Vector2 StartPosition, Vector2 direction)
        {
            Bullet bullet = bulletPool.GetBullet();

            if (bullet != null)
            {
                bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, animation);
            }
        }
    }
}
