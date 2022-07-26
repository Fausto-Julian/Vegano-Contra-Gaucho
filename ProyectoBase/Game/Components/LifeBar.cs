using Game.PhysicsEngine;

namespace Game.Components
{
    public class LifeBar : GameObject
    {
        private readonly HealthController _life;
        private readonly Renderer _rendererBackground;
        private readonly Transform _transformBackground = new Transform();
        private readonly float _maxLife;

        public LifeBar(string ownerId, Vector2 startPosition)
            :base($"LifeBar{ownerId}", new Texture("Texture/LifeBarLine.png"), startPosition + new Vector2(58, 2), Vector2.One, TypeCollision.None)
        {
            _life = GameObjectManager.FindWithTag(ownerId).GetComponent<HealthController>();
            _rendererBackground = new Renderer(this, new Texture("Texture/LifeBarBackground.png"));
            _transformBackground.Position = startPosition;

            _maxLife = _life.MaxHealth / 200;
        }

        public override void Render()
        {
            Transform.Scale = new Vector2(_life.CurrentHealth / _maxLife, Transform.Scale.Y);
            _rendererBackground.Draw(_transformBackground);
            base.Render();
        }
    }
}
