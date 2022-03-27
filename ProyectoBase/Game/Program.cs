using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        public static DateTime StartTime;
        public static float deltaTime;
        private static float lastFrameTime;

        public static int windowWidth = 1920;
        public static int windowHeight = 1080;

        static void Main(string[] args)
        {
            Engine.Initialize("vegans vs gauchos", windowWidth, windowHeight);

            LevelScene levelScene = new LevelScene();

            GameManager.Instance.InitializeScene(levelScene);

            StartTime = DateTime.Now;

            while(true)
            {
                Engine.Clear();

                var currentTime = (float)(DateTime.Now - StartTime).TotalSeconds;
                deltaTime = currentTime - lastFrameTime;

                GameManager.Instance.Update();
                GameManager.Instance.Render();
                
                Engine.Show();

                lastFrameTime = currentTime;
            }
        }
    }
}