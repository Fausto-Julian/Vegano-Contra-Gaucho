using Game.Objects;

namespace Game.Component
{
    public class ShootController
    {
        private readonly PoolGeneric<Bullet> bulletsPool = new PoolGeneric<Bullet>();

        private readonly string ownerId;
        private readonly float speed;
        private readonly float damage;
        private readonly Vector2 direction;

        private readonly Texture texture;
        private readonly string path;
        
        private readonly bool isAnimated;

        public ShootController(string ownerId, string pathAnimation, float speed, float damage, Vector2 direction)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            this.direction = direction;
            path = pathAnimation;
            isAnimated = true;
        }

        public ShootController(string ownerId, string pathAnimation, float speed, float damage)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            path = pathAnimation;
            isAnimated = true;
        }
        
        public ShootController(string ownerId, Texture texture, float speed, float damage, Vector2 direction)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            this.direction = direction;
            this.texture = texture;
            isAnimated = false;
        }

        public ShootController(string ownerId, Texture texture, float speed, float damage)
        {
            this.ownerId = ownerId;
            this.speed = speed;
            this.damage = damage;
            this.texture = texture;
            isAnimated = false;
        }

        public void Shoot(Vector2 startPosition)
        {
            var bullet = isAnimated ? Factory.Instance.CreateBullet(bulletsPool, ownerId, speed, damage, Animation.CreateAnimation(path, 21, true, 0.05f)) : Factory.Instance.CreateBullet(bulletsPool, ownerId, speed, damage, texture);

            bullet?.InitializeBullet(startPosition, direction);
        }

        public void Shoot(Vector2 startPosition, Vector2 direction)
        {
            var bullet = isAnimated ? Factory.Instance.CreateBullet(bulletsPool, ownerId, speed, damage, Animation.CreateAnimation(path, 21, true, 0.05f)) : Factory.Instance.CreateBullet(bulletsPool, ownerId, speed, damage, texture);

            bullet?.InitializeBullet(startPosition, direction);
        }
    }
}
