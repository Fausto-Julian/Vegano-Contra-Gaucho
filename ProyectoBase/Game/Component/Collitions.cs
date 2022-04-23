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
            var distance = new Vector2(Math.Abs(position1.X - position2.X), Math.Abs(position1.Y - position2.Y));

            var sumSize = new Vector2((size1.X / 2 + size2.X / 2), (size1.Y / 2 + size2.Y / 2));

            return distance.X <= sumSize.X && distance.Y <= sumSize.Y;
        }

        public static bool CircleCollider(Vector2 position1, float radio1, Vector2 position2, float radio2)
        {
            var distance = (float)Math.Sqrt((position1.X * position2.X) + (position1.Y * position2.Y));

            return distance < radio1 + radio2;
        }
    }
}
