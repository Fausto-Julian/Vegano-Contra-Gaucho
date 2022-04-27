using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*
     * Class to create objects, contains an id, animation for the object and the position on the screen. Add the update and render function for the object.
     */
    public abstract class GameObject
    {
        public string Id { get; private set; }

        public Texture Texture { get; set; }
        
        public Animation Animation { get; set; }

        public Transform Transform { get; set; } = new Transform();
        
        public Vector2 CenterPosition => IsAnimated ? new Vector2(Transform.Position.X + (Animation.CurrentFrame.Width / 2), Transform.Position.Y + (Animation.CurrentFrame.Height / 2)) : new Vector2(Transform.Position.X + (Texture.Width / 2), Transform.Position.Y + (Texture.Height / 2));

        public Vector2 RealScale => IsAnimated ? new Vector2(Animation.CurrentFrame.Width * Transform.Scale.X, Animation.CurrentFrame.Height * Transform.Scale.Y) : new Vector2(Texture.Width * Transform.Scale.X, Texture.Height * Transform.Scale.Y);

        public bool DontDestroyOnLoad { get; set; }

        public bool IsActive { get; set; }

        public bool IsAnimated { get; private set; }
        
        public BoxCollider BoxCollider { get; set; }

        protected void SetPosition(Vector2 position)
        {
            Transform.Position = position;
        }

        protected GameObject(bool dontDestroyOnLoad = false)
        {
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this);
        }

        protected GameObject(string id, Animation animation, Vector2 startPosition, Vector2 scale, bool dontDestroyOnLoad = false, bool isTrigger = false, float angle = 0)
        {
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this, isTrigger);
            Initialize(id, animation, startPosition, scale, angle);
        }

        protected GameObject(string id, Texture texture, Vector2 startPosition, Vector2 scale, bool dontDestroyOnLoad = false, bool isTrigger = false, float angle = 0)
        {
            DontDestroyOnLoad = dontDestroyOnLoad;
            BoxCollider = new BoxCollider(this, isTrigger);
            Initialize(id, texture, startPosition, scale, angle);
        }

        protected void Initialize(string id, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            Id = id;
            this.Animation = animation;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Rotation = angle;

            IsAnimated = true;

            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        protected void Initialize(string id, Texture texture, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            Id = id;
            this.Texture = texture;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Rotation = angle;

            IsAnimated = false;

            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        public void Destroy()
        {
            GameObjectManager.RemoveGameObject(this);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public virtual void Update()
        {
            if (IsAnimated)
                Animation.Update();
        }

        public virtual void Render()
        {
            Renderer.Draw(IsAnimated ? Animation.CurrentFrame : Texture, Transform);
        }
    }
}
