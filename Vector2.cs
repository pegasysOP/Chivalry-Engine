using System;

namespace ChivalryEngineCore
{
    public class Vector2 : IEquatable<Vector2>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2()
        {
            X = Zero.X;
            Y = Zero.Y;
        }

        // Indexer
        public float this[int index]
        {
            get
            {
                if (index == 0) return X;
                if (index == 1) return Y;
                throw new IndexOutOfRangeException("Vector2 only has 2 dimensions: X and Y");
            }
            set
            {
                if (index == 0) X = value;
                if (index == 1) Y = value;
                throw new IndexOutOfRangeException("Vector2 only has 2 dimensions: X and Y");
            }
        }

        public Vector2 Normalized
        {
            get
            {
                return new Vector2(X / Magnitude, Y / Magnitude);
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }

        public float AngleInRadians
        {
            get
            {
                return (float)Math.Atan2(Y, X);
            }
        }

        public float AngleInDegrees
        {
            get
            {
                return (float)(AngleInRadians * (180 / Math.PI));
            }
        }

        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public float Dot(Vector2 other)
        {
            return X * other.X + Y * other.Y;
        }

        public float Cross(Vector2 other)
        {
            return X * other.Y - Y * other.X;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector && this == vector;
        }
        public bool Equals(Vector2 other)
        {
            return this == other;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{X.ToString()}, {Y.ToString()}";
        }
        public string ToFormattedString(string format)
        {
            return $"{X.ToString(format)}, {Y.ToString(format)}";
        }

        #region Operator Overrides
        // +
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        public static Vector2 operator +(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X + scalar, vector.Y + scalar);
        }
        public static Vector2 operator +(float scalar, Vector2 vector)
        {
            return new Vector2(vector.X + scalar, vector.Y + scalar);
        }
        // -
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.X, -vector.Y);
        }
        // *
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }
        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }
        // /
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
        }
        // ==
        public static bool operator ==(Vector2 left, Vector2 right)
        { 
            return left.X == right.X && left.Y == right.Y;
        }
        // !=
        public static bool operator !=(Vector2 left, Vector2 right)
        { 
            return !(left == right);
        }
        #endregion
    }
}
