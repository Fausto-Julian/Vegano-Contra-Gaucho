using System;

namespace Game
{
    public class Program
    {
        public static float deltaTime { get; private set; }
        public static float RealDeltaTime { get; private set; }
        public static float ScaleTime { get; set; } = 1;

        private static DateTime StartTime;
        private static float lastFrameTime;


        public const int windowWidth = 1920;
        public const int windowHeight = 1080;

        public static MenuTest menuTest { get; set; }
        public static LevelScene levelScene { get; set; }
        public static LevelScene2 levelScene2 { get; set; }
        public static DefeatScene defeatScene { get; set; }

        static void Main(string[] args)
        {
            Engine.Initialize("vegans vs gauchos", windowWidth, windowHeight);

            menuTest = new MenuTest();
            levelScene = new LevelScene();
            levelScene2 = new LevelScene2();
            defeatScene = new DefeatScene();

            GameManager.Instance.AddScene(menuTest);
            GameManager.Instance.AddScene(levelScene);
            GameManager.Instance.AddScene(levelScene2);
            GameManager.Instance.AddScene(defeatScene);

            GameManager.Instance.InitializeGame(Scene.defeat);

            StartTime = DateTime.Now;

            while(true)
            {
                Engine.Clear();

                var currentTime = (float)(DateTime.Now - StartTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;
                RealDeltaTime = deltaTime;
                deltaTime *= ScaleTime;

                GameManager.Instance.Update();
                GameManager.Instance.Render();
                
                Engine.Show();

                lastFrameTime = currentTime;
            }
        }
    }
}