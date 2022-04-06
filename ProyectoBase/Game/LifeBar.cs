using System;

namespace Game
{
    class LifeBar : GameObject
    {
        private HealthController life;
        private Texture textureBackground;
        private Transform transformBackground = new Transform();

        public LifeBar(string ownerID, HealthController life, Texture textureBackground, Texture bar, Vector2 startPosition)
            :base($"LifeBar{ownerID}", bar, startPosition, Vector2.One)
        {
            this.life = life;
            this.textureBackground = textureBackground;
            transformBackground.Position = startPosition - new Vector2(10f, 10f);
            transformBackground.Scale = new Vector2(life.maxHealth + 20, 1f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            transform.Scale = new Vector2(life.currentHealth, transform.Scale.y);
            Renderer.Draw(textureBackground, transformBackground);
            base.Render();
        }

    }
}
