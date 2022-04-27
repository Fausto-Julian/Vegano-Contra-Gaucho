using System;
using System.Collections.Generic;

namespace Game
{
    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance => _instance ?? (_instance = new GameManager());

        private readonly List<IScene> scenes = new List<IScene>();

        private IScene CurrentScene { get; set; }

        public Action OnGamePause;

        public void InitializeGame(Scene sceneId)
        {
            ChangeScene(sceneId);
        }

        public void AddScene(IScene sceneAdd)
        {
            scenes.Add(sceneAdd);
        }

        public void Update()
        {
            CurrentScene.Update();
            GameObjectManager.Update();
        }

        public void Render()
        {
            CurrentScene.Render();
            GameObjectManager.Render();
        }

        public void ChangeScene(Scene id)
        {
            var scene = GetScene(id);

            if (scene != null)
            {
                GameObjectManager.RemoveAllGameObject();
                CurrentScene = scene;
                CurrentScene.Initialize();
                Engine.Debug($"Cambio de scena realizado: Se cambio a {CurrentScene.Id}");
            }
        }

        private IScene GetScene(Scene id)
        {
            for (var i = 0; i < scenes.Count; i++)
            {
                if (scenes[i].Id == id)
                {
                    return scenes[i];
                }
            }
            return null;
        }

        public void SetGamePause(int gameScale)
        {
            gameScale = gameScale > 1 ? 1 : gameScale;
            gameScale = gameScale < 0 ? 0 : gameScale;

            Program.ScaleTime = gameScale;
            OnGamePause?.Invoke();
        }

        public static void ExitGame()
        {
            Environment.Exit(1);
        }
    } 
}
