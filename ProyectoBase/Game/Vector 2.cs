using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {

        public float x { get; set; }
        public float y { get; set; }


        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 One { get; private set; } = new Vector2(1f, 1f);


        public float Magnitud()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Vector2 Normalize()
        {
            return new Vector2(x / Magnitud(), y / Magnitud());
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2) 
        {
            return new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        public static Vector2 operator *(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x * vector2.x, vector1.y * vector2.y);
        }

        public static Vector2 operator *(Vector2 vector1, float val)
        {
            return new Vector2(vector1.x * val, vector1.y * val);
        }

        public static Vector2 operator /(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x / vector2.x, vector1.y / vector2.y);
        }

        public static Vector2 operator /(Vector2 vector1, float val)
        {
            return new Vector2(vector1.x / val, vector1.y / val);
        }

        public static bool operator ==(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x == vector2.x && vector1.y == vector2.y;
        }

        public static bool operator !=(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x != vector2.x && vector1.y != vector2.y;
        }

        

    }
}
