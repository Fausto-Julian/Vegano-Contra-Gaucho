namespace Game.Interface
{
    public interface IKey
    {
        Keys KeyCode { get; }
        void Update();
        bool GetKeyDown();
        bool GetKeyUp();
    }
}