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

            IsTrigger = isTrigger;
        }

        private readonly GameObject myObject;

        public bool IsTrigger { get; set; }

        public bool CheckCollision(out GameObject collider, out bool onTrigger, out bool onCollision)
        {
            collider = null;
            onTrigger = false;
            onCollision = false;

            for (var i = 0; i < GameObjectManager.ActiveGameObjects.Count; i++)
            {
                var obj = GameObjectManager.ActiveGameObjects[i];
                if (obj.Id != myObject.Id && obj.IsActive)
                {
                    var collition = Collitions.BoxCollider(myObject.Transform.Position, myObject.RealScale, obj.Transform.Position, obj.RealScale);

                    if ((IsTrigger || obj.BoxCollider.IsTrigger) && collition)
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
