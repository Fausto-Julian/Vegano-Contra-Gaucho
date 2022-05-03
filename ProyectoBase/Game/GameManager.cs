using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Interface;

namespace Game
{
    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance => _instance ?? (_instance = new GameManager());

        private List<IScene> scenes = new List<IScene>();

        private IScene currentScene;

        public Action OnGamePause;
        
        public void InitializeGame(Interface.SceneId sceneIdId)
        {
            ChangeScene(sceneIdId);
        }

        public void AddScene(IScene sceneAdd)
        {
            scenes.Add(sceneAdd);
        }

        public void Update()
        {
            currentScene.Update();
            GameObjectManager.Update();
        }

        public void Render()
        {
            currentScene.Render();
            GameObjectManager.Render();
        }
        
        public void ChangeScene(Interface.SceneId id)
        {
            var scene = GetScene(id);

            if (scene != null)
            {
                GameObjectManager.RemoveAllGameObject();
                currentScene = scene;
                currentScene.Initialize();
                Engine.Debug($"Cambio de scena realizado: Se cambio a {currentScene.Id}");
            }
        }

        private IScene GetScene(Interface.SceneId id)
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

            if (Program.ScaleTime == gameScale)
                return;

            Program.ScaleTime = gameScale;
            OnGamePause.Invoke();
        }

        public static void ExitGame()
        {
            Environment.Exit(1);
        }
    } 
}
