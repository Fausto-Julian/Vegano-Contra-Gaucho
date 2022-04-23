using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {
        private bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public float X { get; set; }
        public float Y { get; set; }


        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 One { get; private set; } = new Vector2(1f, 1f);


        private float Magnitud()
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
