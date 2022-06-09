namespace Game
{
    public abstract class Component
    {
        public GameObject GameObject { get; }
        protected Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}