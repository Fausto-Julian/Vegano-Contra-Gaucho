using System;
using Game.Components;

namespace Game
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }
        
        public static Vector2 Zero { get; } = new Vector2(0.0f, 0.0f);

        public static Vector2 One { get; } = new Vector2(1f, 1f);

        public static Vector2 Up { get; } = new Vector2(0.0f, 1f);

        public static Vector2 Down { get; } = new Vector2(0.0f, -1f);

        public static Vector2 Left { get; } = new Vector2(-1f, 0.0f);

        public static Vector2 Right { get; } = new Vector2(1f, 0.0f);

        public Vector2(float x, float y)
        {
          X = x;
          Y = y;
        }

        public void Set(float newX, float newY)
        {
          X = newX;
          Y = newY;
        }

        public static Vector2 MoveTowards(
          Vector2 current,
          Vector2 target,
          float maxDistanceDelta)
        {
          var num1 = target.X - current.X;
          var num2 = target.Y - current.Y;
          var d = (float) (num1 * (double) num1 + num2 * (double) num2);
          if (d == 0.0 || maxDistanceDelta >= 0.0 && d <= maxDistanceDelta * (double) maxDistanceDelta)
            return target;
          var num3 = (float) Math.Sqrt(d);
          return new Vector2(current.X + num1 / num3 * maxDistanceDelta, current.Y + num2 / num3 * maxDistanceDelta);
        }

        public static Vector2 Scale(Vector2 a, Vector2 b) => new Vector2(a.X * b.X, a.Y * b.Y);

        public void Scale(Vector2 scale)
        {
          X *= scale.X;
          Y *= scale.Y;
        }

        public void Normalize()
        {
          float magnitude = Magnitude;
          if (magnitude > 9.99999974737875E-06)
          {
            X = X / magnitude;
            Y = Y / magnitude;
          }
          else
          {
            X = 0;
            Y = 0;
          }
        }

        public Vector2 Normalized
        {
          get
          {
            var normalized = new Vector2(X, Y);
            normalized.Normalize();
            return normalized;
          }
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
          float num = -2f * Dot(inNormal, inDirection);
          return new Vector2(num * inNormal.X + inDirection.X, num * inNormal.Y + inDirection.Y);
        }

        public static Vector2 Perpendicular(Vector2 inDirection) => new Vector2(-inDirection.Y, inDirection.X);

        public static float Dot(Vector2 lhs, Vector2 rhs) => (float) (lhs.X * rhs.X + lhs.Y * rhs.Y);

        public float Magnitude => (float) Math.Sqrt(X * X + Y * Y);

        public float sqrMagnitude => (float) (X * X + Y * Y);

        public static float Distance(Vector2 a, Vector2 b)
        {
          float num1 = a.X - b.X;
          float num2 = a.Y - b.Y;
          return (float) Math.Sqrt(num1 * (double) num1 + num2 * (double) num2);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
          float sqrMagnitude = vector.sqrMagnitude;
          if (sqrMagnitude <= maxLength * (double) maxLength)
            return vector;
          float num1 = (float) Math.Sqrt(sqrMagnitude);
          float num2 = vector.X / num1;
          float num3 = vector.Y / num1;
          return new Vector2(num2 * maxLength, num3 * maxLength);
        }

        public static float SqrMagnitude(Vector2 a) => (float) (a.X * (double) a.X + a.Y * (double) a.Y);

        public float SqrMagnitude() => (float) (X * (double) X + Y * (double) Y);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.X * b.X, a.Y * b.Y);
        
        public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.X / b.X, a.Y / b.Y);

        public static Vector2 operator -(Vector2 a) => new Vector2(-a.X, -a.Y);

        public static Vector2 operator *(Vector2 a, float d) => new Vector2(a.X * d, a.Y * d);

        public static Vector2 operator *(float d, Vector2 a) => new Vector2(a.X * d, a.Y * d);

        public static Vector2 operator /(Vector2 a, float d) => new Vector2(a.X / d, a.Y / d);

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
          var num1 = lhs.X - rhs.X;
          var num2 = lhs.Y - rhs.Y;
          return num1 * (double) num1 + num2 * (double) num2 < 9.99999943962493E-11;
        }
        
        public static bool operator !=(Vector2 lhs, Vector2 rhs) => !(lhs == rhs);

        public bool Equals(Vector2 other)
        {
          return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
          if (obj is Vector2 other)
          {
            return Equals(other);
          }

          return false;
        }
        
        internal static Vector2 Transform(Vector2 v, Transform transform)
        {
          return new Vector2(
            transform.Cos * v.X - transform.Sin * v.Y + transform.Position.X, 
            transform.Sin * v.X + transform.Cos * v.Y + transform.Position.Y);
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
          // cz = ax * by − ay * bx
          return a.X * b.Y - a.Y * b.X;
        }

        public override string ToString()
        {
          return $"X: {X}, Y: {Y}";
        }
    }
}