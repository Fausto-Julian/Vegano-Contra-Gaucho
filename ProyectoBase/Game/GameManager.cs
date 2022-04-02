﻿using System;
using System.Collections.Generic;

namespace Game
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        private List<IScene> scenes = new List<IScene>();

        public IScene currentScene { get; private set; }

        public Action OnGamePause;

        public void InitializeGame(Scene sceneID)
        {
            ChangeScene(sceneID);
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

        public void ChangeScene(Scene id)
        {
            IScene scene = GetScene(id);

            if (scene != null)
            {
                GameObjectManager.RemoveAllGameObject();
                currentScene = scene;
                currentScene.Initialize();
                Engine.Debug($"Cambio de scena realizado: Se cambio a {currentScene.ID}");
            }
        }

        public IScene GetScene(Scene id)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i].ID == id)
                {
                    return scenes[i];
                }
            }
            return null;
        }

        public void SetGamePause(int isGamePause)
        {
            Program.ScaleTime = isGamePause;
            OnGamePause?.Invoke();
        }
    }
}
