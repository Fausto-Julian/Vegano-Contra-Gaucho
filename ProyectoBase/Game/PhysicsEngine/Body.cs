using System;
using Game.Components;

namespace Game.PhysicsEngine
{

    public sealed class Body : Component
    {
        private Vector2 _linearVelocity;
        private readonly float _rotationalVelocity;

        private Vector2 _force;
        private Vector2 _acceleration;

        public readonly float Density;
        public readonly float Mass;
        public readonly float InvMass;
        public readonly float Restitution;
        public readonly float Area;

        public readonly bool IsStatic;
        public readonly bool IsKinematic;

        public readonly float Radius;
        public readonly Vector2 Size;
        
        private Vector2 _lastPosition;

        public readonly TypeCollision TypeCollision;

        public Transform Transform { get; }

        public Vector2 LinearVelocity
        {
            get => _linearVelocity;
            internal set { _linearVelocity = value; }
        }
        
        public bool IsTrigger { get; }
        
        public Action<GameObject> OnTrigger;
        public Action<GameObject> OnCollision;

        private Body(GameObject gameObject, Transform transform, float density, float mass, float restitution, float area, 
            bool isStatic, bool isTrigger, bool isKinematic, float radius, Vector2 size, TypeCollision typeCollision)
            :base(gameObject)
        {
            Transform = transform;
            _linearVelocity = Vector2.Zero;
            _rotationalVelocity = 0f;

            _force = Vector2.Zero;

            Density = density;
            Mass = mass;
            Restitution = restitution;
            Area = area;

            IsStatic = isStatic;
            IsKinematic = isKinematic;
            IsTrigger = isTrigger;
            Radius = radius;
            Size = size;
            TypeCollision = typeCollision;

            if(!IsStatic)
            {
                InvMass = 1f / Mass;
            }
            else
            {
                InvMass = 0f;
            }
        }
        
        internal void Step(float time, Vector2 gravity, int iterations)
        {
            if(IsStatic)
            {
                return;
            }

            if (!IsKinematic)
            {
                time /= iterations;
                
                _lastPosition = new Vector2(Transform.Position.X, Transform.Position.Y);
                
                _acceleration = _force / Mass;
            
                _linearVelocity += _acceleration * time;
            
                //_linearVelocity += gravity * time;
                Transform.Position += _linearVelocity * time + 0.5f * _acceleration * time * time;

                Transform.Angle += _rotationalVelocity * time;

                _force = Vector2.Zero;
            }
        }

        public void RestoredLastPosition()
        {
            Transform.Position.X = _lastPosition.X;
            Transform.Position.Y = _lastPosition.Y;
        }
        
        public void RestoredLastPositionInX()
        {
            Transform.Position.X = _lastPosition.X;
        }
        
        public void RestoredLastPositionInY()
        {
            Transform.Position.Y = _lastPosition.Y;
        }
        
        public void Move(Vector2 amount)
        {
            Transform.Position += amount;
        }

        public void MoveTo(Vector2 position)
        {
            Transform.Position = position;
        }

        public void Rotate(float amount)
        {
            Transform.Angle += amount;
        }

        public void AddForce(Vector2 amount)
        {
            _force = amount;
        }

        public static Body CreateCircleBody(GameObject gameObject, Transform transform, float radius, bool isKinematic = false, bool isStatic = false, bool isTrigger = false, float mass = 1f, float density = 1f, float restitution = 0.5f)
        {
            var area = (float)(radius * radius * Math.PI);
            /*
            if(area < World.MIN_BODY_SIZE)
            {
                Debug.Error($"Circle radius is too small. Min circle area is {World.MIN_BODY_SIZE}.");
                return null;
            }

            if(area > World.MAX_BODY_SIZE)
            {
                Debug.Error($"Circle radius is too large. Max circle area is {World.MAX_BODY_SIZE}.");
                return null;
            }

            if (density < World.MIN_DENSITY)
            {
                Debug.Error($"Density is too small. Min density is {World.MIN_DENSITY}");
                return null;
            }

            if (density > World.MAX_DENSITY)
            {
                Debug.Error($"Density is too large. Max density is {World.MAX_DENSITY}");
                return null;
            }*/

            restitution = Mathf.Clamp(restitution, 0f, 1f);

            return new Body(gameObject, transform, density, mass, restitution, area, isStatic, isTrigger, isKinematic, radius, Vector2.Zero, TypeCollision.Circle);
        }

        public static Body CreateBoxBody(GameObject gameObject, Transform transform, Vector2 size, bool isKinematic = false, bool isStatic = false, bool isTrigger = false, float mass = 1f, float density = 1f, float restitution = 0.5f)
        {
            var area = size.X * size.Y;
            /*
            if (area < World.MIN_BODY_SIZE)
            {
                Debug.Error($"Area is too small. Min area is {World.MIN_BODY_SIZE}.");
                return null;
            }

            if (area > World.MAX_BODY_SIZE)
            {
                Debug.Error($"Area is too large. Max area is {World.MAX_BODY_SIZE}.");
                return null;
            }

            if (density < World.MIN_DENSITY)
            {
                Debug.Error($"Density is too small. Min density is {World.MIN_DENSITY}");
                return null;
            }

            if (density > World.MAX_DENSITY)
            {
                Debug.Error($"Density is too large. Max density is {World.MAX_DENSITY}");
                return null;
            }*/

            restitution = Mathf.Clamp(restitution, 0f, 1f);

            return new Body(gameObject, transform, density, mass, restitution, area, isStatic, isTrigger, isKinematic, 0f, size, TypeCollision.Box);
        }
    }
}
