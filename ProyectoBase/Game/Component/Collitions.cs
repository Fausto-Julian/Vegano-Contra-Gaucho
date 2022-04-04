using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // Trae funciones para determinar si dos objetos estan collisionando en la scena.
    public static class Collitions
    {
        public static bool BoxCollider(Vector2 position1, Vector2 size1, Vector2 position2, Vector2 size2)
        {
            var distance = new Vector2(Math.Abs(position1.x - position2.x), Math.Abs(position1.y - position2.y));

            var sumSize = new Vector2((size1.x / 2 + size2.x / 2), (size1.y / 2 + size2.y / 2));

            return distance.x <= sumSize.x && distance.y <= sumSize.y;
        }

        public static bool CircleCollider(Vector2 position1, float radio1, Vector2 position2, float radio2)
        {
            var distance = (float)Math.Sqrt((position1.x * position2.x) + (position1.y * position2.y));

            return distance < radio1 + radio2;
        }
    }
}
