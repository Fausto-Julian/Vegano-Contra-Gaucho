using System;
using System.Collections.Generic;

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

        static void Main(string[] args)
        {
            Engine.Initialize("vegans vs gauchos", windowWidth, windowHeight);

            DefeatScene levelScene = new DefeatScene();

            GameManager.Instance.InitializeScene(levelScene);

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