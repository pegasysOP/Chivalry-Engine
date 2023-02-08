using System.Collections.Concurrent;

namespace ChivalryEngineCore
{
    public class GameObjectDestroyedException : Exception
    {
        public int GameObjectID { get; }
        public GameObjectDestroyedException(int id)
            : base($"GameObject with ID: {id} has been destroyed but you are still trying to access it")
        {
            GameObjectID = id;
            Log.Error($"GameObject ID: {id} has been destroyed but you are still trying to access it");
        }
    }

    public class GameObject : IEquatable<GameObject>
    {
        private static ConcurrentDictionary<int, GameObject> gameObjects = new ConcurrentDictionary<int, GameObject>();
        private static int nextID = 0;

        private bool _isAlive;

        public int ID { get { EnsureAlive(); return _id; } }
        private readonly int _id;
        public Vector2 Position { get { EnsureAlive(); return _position; } set { EnsureAlive(); _position = value; } }
        private Vector2 _position;
        public Vector2 Scale { get { EnsureAlive(); return _scale; } set { EnsureAlive(); _scale = value; } }
        private Vector2 _scale;
        public float Rotation { get { EnsureAlive(); return _rotation; } set { EnsureAlive(); _rotation = value; } }
        private float _rotation;

        public string Tag { get { EnsureAlive(); return _tag; } set { EnsureAlive(); _tag = value; } }
        private string _tag;

        public Collider? Collider { get{ EnsureAlive(); return _collider; } set { EnsureAlive(); _collider = value; } }
        private Collider? _collider;

        public GameObject(Vector2 position, float rotationInDegrees, Vector2 scale)
        {
            _id = Interlocked.Increment(ref nextID);
            _isAlive = true;
            _position = position;
            _rotation = rotationInDegrees;
            _scale = scale;
            _tag = "";
            _collider = null;

            // Keep trying to add the instance until it is successful
            while (!gameObjects.TryAdd(_id, this)) { }
        }
        public GameObject() : this(new Vector2(0f, 0f), 0f, new Vector2(1f, 1f)) { }
        public GameObject(Vector2 position) : this(position, 0f, new Vector2(1f, 1f)) { }
        public GameObject(Vector2 position, float rotation) : this(position, rotation, new Vector2(1f, 1f)) { }

        public void Destroy()
        {
            lock (this)
            {
                EnsureAlive();

                // Keep trying to remove the instance until it is successful
                while (!gameObjects.TryRemove(ID, out _)) { }

                _isAlive = false;

                _position = new Vector2();
                _scale = new Vector2();
                _rotation = 0f;

                _tag = "";

                _collider = null;
            }
        }

        private void EnsureAlive()
        {
            lock (this)
            {
                if (!_isAlive)
                {
                    throw new GameObjectDestroyedException(_id);
                }
            }
        }

        public void Translate(Vector2 translation)
        {
            EnsureAlive();

            Position += translation;
        }

        public void Rotate(float angleInDegrees)
        {
            EnsureAlive();

            Rotation = (Rotation + angleInDegrees) % 360;
            if (Rotation < 0f)
            {
                Rotation += 360;
            }
        }

        #region Overrides
        public override string ToString()
        {
            EnsureAlive();

            return _id.ToString();
        }
        // When '=='/'Equals' are run with this and 'null', if IsAlive is false then the comparison should return true
        // if both GameObjects are still alive, their ID is compared
        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, null))
            {
                return !_isAlive;
            }

            if (!(obj is GameObject other))
            {
                return false;
            }

            return this == other;
        }
        public bool Equals(GameObject? other)
        {
            if (ReferenceEquals(other, null))
            {
                return !_isAlive;
            }

            return this == other;
        }
        public override int GetHashCode()
        {
            EnsureAlive();

            return _id.GetHashCode();
        }

        public static bool operator ==(GameObject left, GameObject right)
        {
            if (ReferenceEquals(left, null) || !left._isAlive)
            {
                return ReferenceEquals(right, null) || !right._isAlive;
            }
            else if (ReferenceEquals(right, null) || !right._isAlive)
            {
                return false;
            }

            return left._id == right._id;
        }
        public static bool operator !=(GameObject left, GameObject right)
        {
            return !(left == right);
        }
        #endregion
    }
}