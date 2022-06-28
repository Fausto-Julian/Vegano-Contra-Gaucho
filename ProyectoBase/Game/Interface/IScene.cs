namespace Game.Interface
{

    public enum SceneId
    {
        Menu,
        Credit,
        SelectMode,
        LevelNormal1,
        LevelNormal2,
        LevelNormal3,
        LevelVegan1,
        LevelVegan2,
        LevelVegan3,
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
