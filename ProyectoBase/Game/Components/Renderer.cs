namespace Game.Components
{
    public class Renderer : Component
    {
        public Texture Texture { get; set; }
        
        public Animation Animation { get; }
        
        public bool IsAnimated { get; }

        public Renderer(GameObject gameObject, Texture texture)
            : base(gameObject)
        {
            Texture = texture;
            IsAnimated = false;
        }
        
        public Renderer(Texture texture)
            : base(null)
        {
            Texture = texture;
            IsAnimated = false;
        }
        
        public Renderer(GameObject gameObject, Animation animation)
            : base(gameObject)
        {
            Animation = animation;
            IsAnimated = true;
        }
        
        public void Draw(Transform transform)
        {
            Engine.Draw(IsAnimated ? Animation.CurrentFrame : Texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Angle);
        }
    }
}
