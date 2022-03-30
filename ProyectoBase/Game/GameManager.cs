using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool IsGamePause { get; private set; }

        public void InitializeScene(IScene LevelScene)
        {
            scenes.Add(LevelScene);

            ChangeScene(Scene.level);
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
                currentScene = scene;
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

        public void SetGamePause(bool isGamePause)
        {
            IsGamePause = isGamePause;
        }
    }
}
