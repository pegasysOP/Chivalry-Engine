namespace ChivalryEngineCore
{
    public class ColliderNotImplementedException : NotImplementedException
    {
        public Type TypeA { get; set; }
        public Type TypeB { get; set; }
        public ColliderNotImplementedException(Collider a, Collider b)
            : base($"Functionality between {a.GetType()} and {b.GetType()} has not been implemented yet")
        {
            TypeA = a.GetType();
            TypeB = b.GetType();

            Log.Error($"Functionality between {TypeA} and {TypeB} has not been implemented yet");
        }
    }

    public abstract class Collider
    {
        public bool IsActive { get; set; } = true;
        public bool IsTrigger { get; set; } = false;
        public string Layer { get; set; } = "Default";

        private bool IntersectsCircles(CircleCollider a, CircleCollider b)
        {
            float centreDistance = a.Centre.Distance(b.Centre);
            return centreDistance < (a.Radius + b.Radius);
        }

        private bool IntersectsBoxes(BoxCollider a, BoxCollider b)
        {
            // If they overlap on the x axis
            if ((b.Position.X >= a.Position.X && b.Position.X <= a.Position.X + a.Size.X)
                || (b.Position.X + b.Size.X >= a.Position.X && b.Position.X + b.Size.X <= a.Position.X + a.Size.X))
            {
                // If they also overlap on the y axis
                if ((b.Position.Y >= a.Position.Y && b.Position.Y <= a.Position.Y + a.Size.Y)
                || (b.Position.Y + b.Size.Y >= a.Position.Y && b.Position.Y + b.Size.Y <= a.Position.Y + a.Size.Y))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IntersectsCircleBox(CircleCollider circle, BoxCollider box)
        {
            // Find the closest point to the circle within the box
            float closestX = Math.Max(box.Position.X, Math.Min(circle.Centre.X,(box.Position.X + box.Size.X)));
            float closestY = Math.Max(box.Position.Y, Math.Min(circle.Centre.Y,(box.Position.Y + box.Size.Y)));

            // Compare the radius of the circle to the distance to the closest point
            float sqrDistance = circle.Centre.SquaredDistance(new Vector2(closestX, closestY));
            return sqrDistance < (circle.Radius * circle.Radius);
        }

        public bool Intersects(Collider other)
        {
            if (this is CircleCollider thisCC)
            {
                if (other is CircleCollider otherCC)
                {
                    return IntersectsCircles(thisCC, otherCC);
                }
                else if(other is BoxCollider otherBC)
                {
                    return IntersectsCircleBox(thisCC, otherBC);
                }
            }
            else if (this is BoxCollider thisBC)
            {
                if(other is BoxCollider otherBC)
                {
                    return IntersectsBoxes(thisBC, otherBC);
                }
                else if (other is CircleCollider otherCC)
                {
                    return IntersectsCircleBox(otherCC, thisBC);
                }
            }

            throw new ColliderNotImplementedException(this, other);
        }


    }

    public class CircleCollider : Collider
    {
        public Vector2 Centre { get; set; } = Vector2.Zero;
        public float Radius { get; set; } = 1f;
    }

    public class BoxCollider : Collider
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set;} = Vector2.Zero;
    }
}
