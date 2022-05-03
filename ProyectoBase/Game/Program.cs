using System;
using Game.Scene;

namespace Game
{
    public static class Program
    {
        public static float DeltaTime { get; private set; }
        public static float RealDeltaTime { get; private set; }
        public static int ScaleTime { get; set; } = 1;
        
        public const int WINDOW_WIDTH = 1920;
        public const int WINDOW_HEIGHT = 1080;
        
        private static DateTime startTime;
        private static float lastFrameTime;

        private static MenuScene MenuScene { get; set; }
        private static CreditScene CreditScene { get; set; }
        private static LevelScene LevelScene { get; set; }
        private static LevelScene2 LevelScene2 { get; set; }
        private static LevelScene3 LevelScene3 { get; set; }
        private static DefeatScene DefeatScene { get; set; }
        private static VictoryScene VictoryScene { get; set; }

        private static void Main(string[] args)
        {
            Initialize();

            startTime = DateTime.Now;

            while(true)
            {
                Engine.Clear();

                var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
                DeltaTime = currentTime - lastFrameTime;
                RealDeltaTime = DeltaTime;
                DeltaTime *= ScaleTime;

                GameManager.Instance.Update();
                GameManager.Instance.Render();
                
                Engine.Show();

                lastFrameTime = currentTime;
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void Initialize()
        {
            Engine.Initialize("gauchos vs veganos", WINDOW_WIDTH, WINDOW_HEIGHT, true);

            MenuScene = new MenuScene();
            CreditScene = new CreditScene();
            LevelScene = new LevelScene();
            LevelScene2 = new LevelScene2();
            LevelScene3 = new LevelScene3();
            DefeatScene = new DefeatScene();
            VictoryScene = new VictoryScene();

            GameManager.Instance.AddScene(MenuScene);
            GameManager.Instance.AddScene(CreditScene);
            GameManager.Instance.AddScene(LevelScene);
            GameManager.Instance.AddScene(LevelScene2);
            GameManager.Instance.AddScene(LevelScene3);
            GameManager.Instance.AddScene(DefeatScene);
            GameManager.Instance.AddScene(VictoryScene);

            GameManager.Instance.InitializeGame(Interface.SceneId.Menu);
        }
    }
}