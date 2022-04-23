using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    // This class stores all the objects that are in the scenes and provides functions to add or remove elements.
    // Inside the GameManager this class is called and the update or render is called which in turn calls all the update and render
    // of your objects that are added to your list.
    public static class GameObjectManager
    {
        public static List<GameObject> ActiveGameObjects { get; private set; } = new List<GameObject>();

        public static void AddGameObject(GameObject gameObject)
        {
            if (ActiveGameObjects.Contains(gameObject)) 
                return;
            
            ActiveGameObjects.Add(gameObject);
            Engine.Debug($"GamObject add. ID: {gameObject.Id}");
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            if (!ActiveGameObjects.Contains(gameObject)) 
                return;

            ActiveGameObjects.Remove(gameObject);
            Engine.Debug($"Removed GamObject. ID: {gameObject.Id}");
        }

        public static void RemoveAllGameObject()
        {
            for (var i = ActiveGameObjects.Count -1; i >= 0; i--)
            {
                if (!ActiveGameObjects[i].DontDestroyOnLoad)
                    ActiveGameObjects.Remove(ActiveGameObjects[i]);
            }
        }

        public static GameObject FindWithTag(string id)
        {
            foreach (var t in ActiveGameObjects)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }

            return null;
        }

        public static void Render()
        {
            foreach (var gameObject in ActiveGameObjects)
            {
                if (gameObject.IsActive)
                {
                    gameObject.Render();
                }
            }
        }

        public static void Update()
        {
            for (var i = 0; i < ActiveGameObjects.Count; i++)
            {
                if (ActiveGameObjects[i].IsActive)
                {
                    ActiveGameObjects[i].Update();
                }
            }
        }
    }
}
