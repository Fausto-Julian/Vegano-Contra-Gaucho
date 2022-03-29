using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class GameObjectManager
    {

        public static List<GameObject> ActiveGameObjects { get; } = new List<GameObject>();


        public static void AddGameObject(GameObject gameObject)
        {
            ActiveGameObjects.Add(gameObject);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            if (!ActiveGameObjects.Contains(gameObject)) 
                return;

            Engine.Debug($"GamObject removido. ID: {gameObject.ID}");
            ActiveGameObjects.Remove(gameObject);
        }

        public static void Render()
        {
            for (int i = 0; i < ActiveGameObjects.Count; i++)
            {
                if (ActiveGameObjects[i].IsActive)
                {
                    ActiveGameObjects[i].Render();
                }
            }
        }

        public static void Update()
        {
            for (int i = 0; i < ActiveGameObjects.Count; i++)
            {
                if (ActiveGameObjects[i].IsActive)
                {
                    ActiveGameObjects[i].Update();
                }
            }
        }
    }
}
