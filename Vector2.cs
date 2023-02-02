namespace ChivalryEngineCore
{
    public class Vector2
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
        public static Vector2 Zero
        { get { return new Vector2(0, 0); } }

        public Vector2 Normalized
        {
            get
            { 
                float magnitude = (float)Math.Sqrt(X * X + Y * Y);
                return new Vector2(X / magnitude, Y / magnitude);
            }
        }

    }
}
