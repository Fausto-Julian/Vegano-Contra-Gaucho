using Game.PhysicsEngine;

namespace Game.Components
{
    public class LifeBar : GameObject
    {
        private readonly HealthController _life;
        private readonly Renderer _rendererBackground;
        private readonly Transform _transformBackground = new Transform();

        public LifeBar(string ownerId, Texture textureBackground, Texture background, Vector2 startPosition)
            :base($"LifeBar{ownerId}", background, startPosition, Vector2.One, TypeCollision.None)
        {
            _life = GameObjectManager.FindWithTag(ownerId).GetComponent<HealthController>();
            _rendererBackground = new Renderer(this, textureBackground);
            _transformBackground.Position = startPosition - new Vector2(10f, 10f);
            _transformBackground.Scale = new Vector2(_life.MaxHealth * 2 + 20, 1f);
        }

        public override void Render()
        {
            Transform.Scale = new Vector2(_life.CurrentHealth * 2, Transform.Scale.Y);
            _rendererBackground.Draw(_transformBackground);
            base.Render();
        }
    }
}
