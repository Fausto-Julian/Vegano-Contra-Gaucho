namespace Game.Interface
{

    public enum SceneId
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
        SceneId Id { get; }

        void Initialize();

        void Update();

        void Render();
    }
}
