using System;
using Game.Scene;

namespace Game
{
    public static class Program
    {
        public static float DeltaTime { get; private set; }
        public static float RealDeltaTime { get; private set; }
        public static float ScaleTime { get; set; } = 1;

        private static DateTime _startTime;
        private static float _lastFrameTime;

        public const int WINDOW_WIDTH = 1920;
        public const int WINDOW_HEIGHT = 1080;

        private static MenuScene _menuScene;
        private static CreditScene _creditScene;
        private static SelectModeScene _selectModeScene;
        private static LevelNormalScene1 _levelNormalScene1;
        private static LevelNormalScene2 _levelNormalScene2;
        private static LevelNormalScene3 _levelNormalScene3;
        private static LevelVeganScene1 _levelVeganScene1;
        private static LevelVeganScene2 _levelVeganScene2;
        private static LevelVeganScene3 _levelVeganScene3;
        private static DefeatScene _defeatScene;
        private static VictoryScene _victoryScene;

        private static void Main(string[] args)
        {
            Initialize();

            _startTime = DateTime.Now;

            while(true)
            {
                Engine.Clear();

                var currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
                DeltaTime = currentTime - _lastFrameTime;
                RealDeltaTime = DeltaTime;
                DeltaTime *= ScaleTime;
                
                Input.Update();
                
                GameManager.Instance.Update();
                GameManager.Instance.Render();

                Engine.Show();

                _lastFrameTime = currentTime;
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void Initialize()
        {
            Engine.Initialize("gauchos vs veganos", WINDOW_WIDTH, WINDOW_HEIGHT, false);

            _menuScene = new MenuScene();
            _creditScene = new CreditScene();
            _selectModeScene = new SelectModeScene();
            _levelNormalScene1 = new LevelNormalScene1();
            _levelNormalScene2 = new LevelNormalScene2();
            _levelNormalScene3 = new LevelNormalScene3();
            _levelVeganScene1 = new LevelVeganScene1();
            _levelVeganScene2 = new LevelVeganScene2();
            _levelVeganScene3 = new LevelVeganScene3();
            _defeatScene = new DefeatScene();
            _victoryScene = new VictoryScene();

            GameManager.Instance.AddScene(_menuScene);
            GameManager.Instance.AddScene(_creditScene);
            GameManager.Instance.AddScene(_selectModeScene);
            GameManager.Instance.AddScene(_levelNormalScene1);
            GameManager.Instance.AddScene(_levelNormalScene2);
            GameManager.Instance.AddScene(_levelNormalScene3);
            GameManager.Instance.AddScene(_levelVeganScene1);
            GameManager.Instance.AddScene(_levelVeganScene2);
            GameManager.Instance.AddScene(_levelVeganScene3);
            GameManager.Instance.AddScene(_defeatScene);
            GameManager.Instance.AddScene(_victoryScene);

            GameManager.Instance.InitializeGame(Interface.SceneId.Menu);
            
            Input.Initialize();
        }
    }
}