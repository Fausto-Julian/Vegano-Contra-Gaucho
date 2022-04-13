using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BoxCollider
    {

        public BoxCollider(GameObject gameObject)
        {
            myObject = gameObject;
        }

        public BoxCollider(GameObject gameObject, bool isTrigger)
        {
            myObject = gameObject;

            this.isTrigger = isTrigger;
        }

        private GameObject myObject;

        public bool isTrigger { get; set; }

        public bool CheckCollision(out GameObject collider, out bool onTrigger, out bool onCollision)
        {
            collider = null;
            onTrigger = false;
            onCollision = false;

            for (int i = 0; i < GameObjectManager.activeGameObjects.Count; i++)
            {
                var obj = GameObjectManager.activeGameObjects[i];
                if (obj.ID != myObject.ID && obj.IsActive)
                {
                    var collition = Collitions.BoxCollider(myObject.transform.Position, myObject.RealScale, obj.transform.Position, obj.RealScale);

                    if ((isTrigger || obj.boxCollider.isTrigger) && collition)
                    {
                        onTrigger = true;
                        collider = obj;
                        return true;
                    }
                    else if (collition)
                    {
                        onCollision = true;
                        collider = obj;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
