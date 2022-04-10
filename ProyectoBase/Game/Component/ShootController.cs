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
        private Texture texture;

        public ShootController(string OwnerId, float Speed, float Damage, Vector2 Direction, Texture texture)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            direction = Direction;
            this.texture = texture;
        }

        public ShootController(string OwnerId, float Speed, float Damage, Texture texture)
        {
            ownerId = OwnerId;
            speed = Speed;
            damage = Damage;
            this.texture = texture;
        }

        public void Shoot(Vector2 StartPosition)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, texture);
            }
        }

        public void Shoot(Vector2 StartPosition, Vector2 direction)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                bullet.InitializeBullet(ownerId, speed, damage, direction, StartPosition, texture);
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
