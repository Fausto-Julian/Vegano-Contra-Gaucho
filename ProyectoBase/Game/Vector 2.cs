using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {

        public float X { get; set; }
        public float Y { get; set; }


        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 One { get; private set; } = new Vector2(1f, 1f);


        public float Magnitud()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public Vector2 Normalize()
        {
            return new Vector2(X / Magnitud(), Y / Magnitud());
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2) 
        {
            return new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static Vector2 operator *(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X * vector2.X, vector1.Y * vector2.Y);
        }

        public static Vector2 operator *(Vector2 vector1, float val)
        {
            return new Vector2(vector1.X * val, vector1.Y * val);
        }

        public static Vector2 operator /(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X / vector2.X, vector1.Y / vector2.Y);
        }

        public static Vector2 operator /(Vector2 vector1, float val)
        {
            return new Vector2(vector1.X / val, vector1.Y / val);
        }

        public static bool operator ==(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }

        public static bool operator !=(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X != vector2.X && vector1.Y != vector2.Y;
        }

        

    }
}
