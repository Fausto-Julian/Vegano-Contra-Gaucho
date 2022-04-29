namespace Game.Interface
{

    public enum Scene
    {
        Menu,
        Credit,
        Level,
        Level2,
        Level3,
        Defeat,
        Victory
    }

    public interface IScene
    {
        Scene Id { get; }

        void Initialize();

        void Update();

        void Render();
    }
}
