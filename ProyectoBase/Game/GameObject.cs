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

        public Animation Animation { get; set; }

        public Transform transform { get; set; } = new Transform();

        public bool IsActive { get; set; }

        public void SetPosition(Vector2 position)
        {
            transform.Position = position;
        }

        public GameObject(string id, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            Initialize(id, animation, startPosition, scale, angle);
        }

        public void Initialize(string id, Animation animation, Vector2 startPosition, Vector2 scale, float angle = 0)
        {
            ID = id;
            Animation = animation;
            transform.Position = startPosition;
            transform.Scale = scale;
            transform.Rotation = angle;

            GameObjectManager.AddGameObject(this);
            SetActive(true);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public virtual void Update()
        {
            Animation.Update();
        }

        public virtual void Render()
        {
            Renderer.Draw(Animation.currentFrame, transform);
        }
    }
}
