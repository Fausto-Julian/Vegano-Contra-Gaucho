using System;
using System.Collections.Generic;
using Game.Interface;
using Game.PhysicsEngine;

namespace Game
{
    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance => _instance ?? (_instance = new GameManager());

        public World World = new World();

        private readonly List<IScene> _scenes = new List<IScene>();

        private IScene CurrentScene { get; set; }

        public bool ModeVegan { get; set; }
        
        public void InitializeGame(SceneId sceneIdId)
        {
            ChangeScene(sceneIdId);
        }

        public void AddScene(IScene sceneAdd)
        {
            _scenes.Add(sceneAdd);
        }

        public void Update()
        {
            World.Step(Program.DeltaTime, 128);
            CurrentScene.Update();
            GameObjectManager.Update();
        }

        public void Render()
        {
            CurrentScene.Render();
            GameObjectManager.Render();
        }
        
        public void ChangeScene(SceneId id)
        {
            var scene = GetScene(id);

            if (scene != null)
            {
                World.RemoveAllBody();
                Factory.Instance.ClearList();
                GameObjectManager.RemoveAllGameObject();
                CurrentScene = scene;
                CurrentScene.Initialize();
                Debug.Info($"Cambio de scena realizado: Se cambio a {CurrentScene.Id}");
            }
        }

        private IScene GetScene(SceneId id)
        {
            for (var i = 0; i < _scenes.Count; i++)
            {
                if (_scenes[i].Id == id)
                {
                    return _scenes[i];
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
        }

        public static void ExitGame()
        {
            Environment.Exit(1);
        }
    } 
}
