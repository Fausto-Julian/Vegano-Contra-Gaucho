using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*
     * Clase para crear objetos, contiene una id, animacion para el objeto y la posicion en la pantalla. Agrega la funcion update y render para el objeto.
     */
    public abstract class GameObject
    {
        public String ID { get; set; }

        public Texture texture { get; set; }

        public Animation animation { get; set; }

        public Transform transform { get; set; } = new Transform();

        public Vector2 RealScale
        {
            get
            {
                if (IsAnimation)
                {
                    return new Vector2(animation.currentFrame.Width * transform.Scale.x, animation.currentFrame.Height * transform.Scale.y);
                }
                else
                {
                    return new Vector2(texture.Width * transform.Scale.x, texture.Height * transform.Scale.y);
                }
            }
        }

        public bool dontDestroyOnLoad { get; set; }

        public bool IsActive { get; set; }

        private bool IsAnimation;

        public void SetPosition(Vector2 position)
        {
            transform.Position = position;
        }

        public GameObject() { }

        public GameObject(string id, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            Initialize(id, animation, startPosition, scale, angle);
        }

        public GameObject(string id, Texture texture, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            Initialize(id, texture, startPosition, scale, angle);
        }

        public void Initialize(string id, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            ID = id;
            this.animation = animation;
            transform.Position = startPosition;
            transform.Scale = scale;
            transform.Rotation = angle;

            IsAnimation = true;

            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        public void Initialize(string id, Texture texture, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            ID = id;
            this.texture = texture;
            transform.Position = startPosition;
            transform.Scale = scale;
            transform.Rotation = angle;

            IsAnimation = false;

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
            if (IsAnimation)
                animation.Update();
        }

        public virtual void Render()
        {
            if (IsAnimation)
            {
                Renderer.Draw(animation.currentFrame, transform);
            }
            else
            {
                Renderer.Draw(texture, transform);
            }
            
        }
    }
}
