using System.Collections.Generic;
using Game.Components;
using Game.PhysicsEngine;

namespace Game
{
    /*
     * Class to create objects, contains an id, animation for the object and the position on the screen. Add the update and render function for the object.
     */
    public abstract class GameObject
    {
        public string Id { get; }

        public Transform Transform { get; } = new Transform();

        public Vector2 RealSize => Renderer.IsAnimated ? new Vector2(Renderer.Animation.CurrentFrame.Width * Transform.Scale.X, Renderer.Animation.CurrentFrame.Height * Transform.Scale.Y) : new Vector2(Renderer.Texture.Width * Transform.Scale.X, Renderer.Texture.Height * Transform.Scale.Y);

        public bool DontDestroyOnLoad { get; set; }

        public bool IsActive { get; private set; }
        protected Renderer Renderer { get; }

        protected List<Component> Components { get; } = new List<Component>();

        protected GameObject(string id, Animation animation, Vector2 startPosition, Vector2 scale, TypeCollision typeCollision, bool isKinematic = false, bool isStatic = false, bool isTrigger = false, float angle = 0, bool dontDestroyOnLoad = false)
        {
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Angle = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;
            Renderer = new Renderer(this, animation);
            Components.Add(Renderer);
            
            switch (typeCollision)
            {
                case TypeCollision.Box:
                    var boxBody = Body.CreateBoxBody(this, Transform, RealSize, isKinematic, isStatic, isTrigger);
                    GameManager.Instance.World.AddBody(boxBody);
                    Components.Add(boxBody);
                    break;
                case TypeCollision.Circle:
                    var circleBody = Body.CreateCircleBody(this, Transform, RealSize.X / 2, isKinematic, isStatic, isTrigger);
                    GameManager.Instance.World.AddBody(circleBody);
                    Components.Add(circleBody);
                    break;
            }
            
            
            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        protected GameObject(string id, Texture texture, Vector2 startPosition, Vector2 scale, TypeCollision typeCollision, bool isKinematic = false, bool isStatic = false, bool isTrigger = false, float angle = 0, bool dontDestroyOnLoad = false)
        {
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Angle = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;
            Renderer = new Renderer(this, texture);
            Components.Add(Renderer);
            
            switch (typeCollision)
            {
                case TypeCollision.Box:
                    var boxBody = Body.CreateBoxBody(this, Transform, RealSize, isKinematic, isStatic, isTrigger);
                    GameManager.Instance.World.AddBody(boxBody);
                    Components.Add(boxBody);
                    break;
                case TypeCollision.Circle:
                    var circleBody = Body.CreateCircleBody(this, Transform, RealSize.X / 2, isKinematic, isStatic, isTrigger);
                    GameManager.Instance.World.AddBody(circleBody);
                    Components.Add(circleBody);
                    break;
            }
            
            GameObjectManager.AddGameObject(this);
            SetActive(true);
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

        public bool CompareTag(string tag)
        {
            return string.Equals(Id, tag);
        }

        public T GetComponent<T>()
        {
            for (var i = 0; i < Components.Count; i++)
            {
                if (Components[i] is T value)
                {
                    return value;
                }
            }

            return default;
        } 
    }
}
