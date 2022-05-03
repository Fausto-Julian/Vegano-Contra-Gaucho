namespace Game.Component
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
                    var collision = Collisions.IsBoxWithCircleColliding(obj.Transform.Position, obj.RealSize, myObject.Transform.Position, myObject.RealSize.X / 2);

                    if ((IsTrigger || obj.BoxCollider.IsTrigger) && collision)
                    {
                        onTrigger = true;
                        collider = obj;
                        return true;
                    }
                    
                    if (collision)
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
