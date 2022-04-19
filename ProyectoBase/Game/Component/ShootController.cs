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

        private bool isAnimated;

        private string path;

        public ShootController(string OwnerId, string pathTextureAndAnimation, float Speed, float Damage, Vector2 Direction, bool isAnimated)
        {
            ownerId = OwnerId;
            path = pathTextureAndAnimation;
            speed = Speed;
            damage = Damage;
            direction = Direction;
            this.isAnimated = isAnimated;
        }

        public ShootController(string OwnerId, string pathTextureAndAnimation, float Speed, float Damage, bool isAnimated)
        {
            ownerId = OwnerId;
            path = pathTextureAndAnimation;
            speed = Speed;
            damage = Damage;
            this.isAnimated = isAnimated;
        }

        public void Shoot(Vector2 StartPosition)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                bullet.Trayectory(StartPosition, direction);
            }
        }

        public void Shoot(Vector2 StartPosition, Vector2 direction)
        {
            Bullet bullet = CreateBullet();

            if (bullet != null)
            {
                bullet.Trayectory(StartPosition, direction);
            }
        }

        private Bullet CreateBullet()
        {
            var bullet = bulletsPool.GetorCreate();

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet();
                if (isAnimated)
                {
                    bullet.Value.InitializeBullet(ownerId, speed, damage, Animation.CreateAnimation(path, 21, "Idle", true, 0.01f));
                }
                else
                {
                    bullet.Value.InitializeBullet(ownerId, speed, damage, new Texture(path));
                }

                bullet.Value.OnDesactivate += () =>
                {
                    if (bulletsPool.AvailablesCount > 15)
                    {
                        bullet.Value.Destroy();
                    }
                    else
                    {
                        bullet.Value.SetActive(false);
                        bulletsPool.AddPool(bullet);
                    }
                };
            }
            bullet.Value.SetActive(true);
            return bullet.Value;
        }
    }
}
