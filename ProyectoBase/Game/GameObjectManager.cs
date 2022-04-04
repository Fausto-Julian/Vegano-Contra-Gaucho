using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    // Esta clase guarda todos los objetos que estan en las scenas y trae funciones para agregar o remover elementos.
    // Dentro del GameManager se llama esta clase y se llama al update o render que a su vez llama a todos los update y render
    // de sus objetos que esten agregados a su lista.
    public static class GameObjectManager
    {
        public static List<GameObject> activeGameObjects { get; private set; } = new List<GameObject>();

        public static void AddGameObject(GameObject gameObject)
        {
            if (!activeGameObjects.Contains(gameObject))
            {
                activeGameObjects.Add(gameObject);
                Engine.Debug($"GamObject add. ID: {gameObject.ID}");
            }
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            if (!activeGameObjects.Contains(gameObject)) 
                return;

            Engine.Debug($"GamObject removido. ID: {gameObject.ID}");
            activeGameObjects.Remove(gameObject);
        }

        public static void RemoveAllGameObject()
        {
            for (int i = activeGameObjects.Count -1; i >= 0; i--)
            {
                if (!activeGameObjects[i].dontDestroyOnLoad)
                    activeGameObjects.Remove(activeGameObjects[i]);
            }
        }

        public static void Render()
        {
            for (int i = 0; i < activeGameObjects.Count; i++)
            {
                if (activeGameObjects[i].IsActive)
                {
                    activeGameObjects[i].Render();
                }
            }
        }

        public static void Update()
        {
            for (int i = 0; i < activeGameObjects.Count; i++)
            {
                if (activeGameObjects[i].IsActive)
                {
                    activeGameObjects[i].Update();
                }
            }
        }
    }
}
