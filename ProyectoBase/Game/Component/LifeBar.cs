namespace Game.Component
{
    public class LifeBar : GameObject
    {
        private HealthController life;
        private Renderer rendererBackground;
        private Transform transformBackground = new Transform();

        public LifeBar(string ownerId, HealthController life, Texture textureBackground, Texture bar, Vector2 startPosition)
            :base($"LifeBar{ownerId}", bar, startPosition, Vector2.One)
        {
            this.life = life;
            rendererBackground = new Renderer(textureBackground);
            transformBackground.Position = startPosition - new Vector2(10f, 10f);
            transformBackground.Scale = new Vector2(life.MaxHealth * 2 + 20, 1f);
        }

        public override void Render()
        {
            Transform.Scale = new Vector2(life.CurrentHealth * 2, Transform.Scale.Y);
            rendererBackground.Draw(transformBackground);
            base.Render();
        }

    }
}
