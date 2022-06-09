namespace Game.Components
{
    public class ShootController : Component
    {
        private readonly string _ownerId;
        private readonly float _speed;
        private readonly float _damage;
        private readonly Vector2 _direction;

        private readonly Texture _texture;
        private readonly string _path;
        
        private readonly bool _isAnimated;

        public ShootController(GameObject gameObject, string ownerId, string pathAnimation, float speed, float damage, Vector2 direction)
            : base(gameObject)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;
            _direction = direction;
            _path = pathAnimation;
            _isAnimated = true;
        }

        public ShootController(GameObject gameObject, string ownerId, string pathAnimation, float speed, float damage)
            : base(gameObject)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;
            _path = pathAnimation;
            _isAnimated = true;
        }
        
        public ShootController(string ownerId, Texture texture, float speed, float damage, Vector2 direction)
            : base(null)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;
            _direction = direction;
            _texture = texture;
            _isAnimated = false;
        }

        public ShootController(GameObject gameObject, string ownerId, Texture texture, float speed, float damage)
            : base(gameObject)
        {
            _ownerId = ownerId;
            _speed = speed;
            _damage = damage;
            _texture = texture;
            _isAnimated = false;
        }

        public void Shoot(Vector2 startPosition)
        {
            var bullet = _isAnimated ? Factory.Instance.CreateBullet(_ownerId, _speed, _damage, Animation.CreateAnimation(_path, 21, true, 0.05f)) : Factory.Instance.CreateBullet(_ownerId, _speed, _damage, _texture);

            bullet?.InitializeBullet(startPosition, _direction);
        }

        public void Shoot(Vector2 startPosition, Vector2 direction)
        {
            var bullet = _isAnimated ? Factory.Instance.CreateBullet(_ownerId, _speed, _damage, Animation.CreateAnimation(_path, 21, true, 0.05f)) : Factory.Instance.CreateBullet(_ownerId, _speed, _damage, _texture);

            bullet?.InitializeBullet(startPosition, direction);
        }
    }
}
