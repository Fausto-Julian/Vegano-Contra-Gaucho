namespace Game.Component
{
    public class Renderer
    {
        public Texture Texture { get; set; }
        
        public Animation Animation { get; }
        
        public bool IsAnimated { get; }

        public Renderer(Texture texture)
        {
            Texture = texture;
            IsAnimated = false;
        }
        
        public Renderer(Animation animation)
        {
            Animation = animation;
            IsAnimated = true;
        }
        
        public void Draw(Transform transform)
        {
            Engine.Draw(IsAnimated ? Animation.CurrentFrame : Texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Rotation);
        }
    }
}
