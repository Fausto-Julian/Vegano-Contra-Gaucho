using Game.Component;

namespace Game
{
    /*
     * Class to create objects, contains an id, animation for the object and the position on the screen. Add the update and render function for the object.
     */
    public abstract class GameObject
    {
        public string Id { get; private set; }

        public Transform Transform { get; } = new Transform();

        public Vector2 RealSize => Renderer.IsAnimated ? new Vector2(Renderer.Animation.CurrentFrame.Width * Transform.Scale.X, Renderer.Animation.CurrentFrame.Height * Transform.Scale.Y) : new Vector2(Renderer.Texture.Width * Transform.Scale.X, Renderer.Texture.Height * Transform.Scale.Y);

        public bool DontDestroyOnLoad { get; }

        public bool IsActive { get; private set; }

        public BoxCollider BoxCollider { get; }

        protected Renderer Renderer { get; }

        protected GameObject(bool dontDestroyOnLoad = false)
        {
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this);
        }

        protected GameObject(string id, Animation animation, Vector2 startPosition, Vector2 scale, bool dontDestroyOnLoad = false, bool isTrigger = false, float angle = 0)
        {
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Rotation = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this, isTrigger);
            Renderer = new Renderer(animation); 
            
            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        protected GameObject(string id, Texture texture, Vector2 startPosition, Vector2 scale, bool dontDestroyOnLoad = false, bool isTrigger = false, float angle = 0)
        {
            
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Rotation = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this, isTrigger);
            Renderer = new Renderer(texture);
            
            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }
        
        protected void SetPosition(Vector2 position)
        {
            Transform.Position = position;
        }

        protected void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public virtual void Update()
        {
            if (Renderer.IsAnimated)
                Renderer.Animation.Update();
        }

        public virtual void Render()
        {
            Renderer.Draw(Transform);
        }
    }
}
