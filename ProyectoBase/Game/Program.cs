using System;

namespace Game
{
    public static class Program
    {
        public static float DeltaTime { get; private set; }
        public static float RealDeltaTime { get; private set; }
        public static float ScaleTime { get; set; } = 1;

        private static DateTime _startTime;
        private static float _lastFrameTime;

        public const int WindowWidth = 1920;
        public const int WindowHeight = 1080;

        private static MenuScene MenuScene { get; set; }
        private static CreditScene CreditScene { get; set; }
        private static LevelScene LevelScene { get; set; }
        public static LevelScene2 LevelScene2 { get; set; }
        private static DefeatScene DefeatScene { get; set; }
        private static VictoryScene VictoryScene { get; set; }

        static void Main(string[] args)
        {
            Engine.Initialize("vegans vs gauchos", WindowWidth, WindowHeight);

            MenuScene = new MenuScene();
            CreditScene = new CreditScene();
            LevelScene = new LevelScene();
            LevelScene2 = new LevelScene2();
            DefeatScene = new DefeatScene();
            VictoryScene = new VictoryScene();

            GameManager.Instance.AddScene(MenuScene);
            GameManager.Instance.AddScene(CreditScene);
            GameManager.Instance.AddScene(LevelScene);
            GameManager.Instance.AddScene(LevelScene2);
            GameManager.Instance.AddScene(DefeatScene);
            GameManager.Instance.AddScene(VictoryScene);

            GameManager.Instance.InitializeGame(Scene.Menu);

            _startTime = DateTime.Now;

            while(true)
            {
                Engine.Clear();

                var currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
                DeltaTime = currentTime - _lastFrameTime;
                RealDeltaTime = DeltaTime;
                DeltaTime *= ScaleTime;

                GameManager.Instance.Update();
                GameManager.Instance.Render();
                
                Engine.Show();

                _lastFrameTime = currentTime;
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}