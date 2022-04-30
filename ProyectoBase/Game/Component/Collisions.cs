using System;

namespace Game.Component
{
    // It brings functions to determine if two objects are colliding in the scene.
    public static class Collisions
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

        public static bool IsBoxWithCircleColliding(Vector2 boxPosition, Vector2 boxSize, Vector2 circlePosition,
            float circleRadius)
        {
            var position = circlePosition;
            if (circlePosition.X < boxPosition.X)
            {
                position.X = boxPosition.X;
            }
            else if (circlePosition.X > boxPosition.X + boxSize.X)
            {
                position.X = boxPosition.X + boxSize.X;
            }
            
            if (circlePosition.Y < boxPosition.Y)
            {
                position.Y = boxPosition.Y;
            }
            else if (circlePosition.Y > boxPosition.Y + boxSize.Y)
            {
                position.Y = boxPosition.Y + boxSize.Y;
            }

            var vectorDistance = circlePosition - position;

            var distance = (float)Math.Sqrt(vectorDistance.X * vectorDistance.X + vectorDistance.Y * vectorDistance.Y);

            return distance <= circleRadius;
        }
    }
}
