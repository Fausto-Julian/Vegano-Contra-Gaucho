using System;
using System.Collections.Generic;

namespace Game.PhysicsEngine
{
    public sealed class World
    {
        public const float MIN_BODY_SIZE = 0.01f * 0.01f;
        public const float MAX_BODY_SIZE = 2000f * 2000f;

        public const float MIN_DENSITY = 0.5f; // g/cm^3
        public const float MAX_DENSITY = 21.4f;

        public const int MIN_ITERATIONS = 1;
        public const int MAX_ITERATIONS = 128;

        private readonly Vector2 _gravity;
        private readonly List<Body> _bodyList;

        public int BodyCount => _bodyList.Count;

        public World()
        {
            _gravity = new Vector2(0f, 9.81f);
            _bodyList = new List<Body>();
        }

        public void AddBody(Body body)
        {
            _bodyList.Add(body);
        }

        public void RemoveBody(Body body)
        {
            if (_bodyList.Contains(body))
            {
                _bodyList.Remove(body);
            }
            else
            {
                Debug.Error("Error trying to delete body");
            }
        }
        
        public void RemoveAllBody()
        {
            for (var i = 0; i < _bodyList.Count; i++)
            {
                _bodyList.RemoveAt(i);
            }
        }

        public bool GetBody(int index, out Body body)
        {
            body = null;

            if(index < 0 || index >= _bodyList.Count)
            {
                return false;
            }

            body = _bodyList[index];
            return true;
        }

        public void Step(float time, int iterations)
        {
            iterations = Mathf.Clamp(iterations, MIN_ITERATIONS, MAX_ITERATIONS);

            for (var it = 0; it < iterations; it++)
            {
                // Movement step
                for (var i = 0; i < _bodyList.Count; i++)
                {
                    _bodyList[i].Step(time, _gravity, iterations);
                }

                // Collision step
                for (var i = 0; i < _bodyList.Count - 1; i++)
                {
                    var bodyA = _bodyList[i];
                    if (!bodyA.GameObject.IsActive)
                    {
                        continue;
                    }

                    for (var j = i + 1; j < _bodyList.Count; j++)
                    {
                        var bodyB = _bodyList[j];

                        if (bodyA.IsStatic && bodyB.IsStatic)
                        {
                            continue;
                        }

                        if (!bodyB.GameObject.IsActive)
                        {
                            continue;
                        }

                        if (bodyA.GameObject.Id == bodyB.GameObject.Id)
                        {
                            continue;
                        }

                        if (Collide(bodyA, bodyB))
                        {
                            if (bodyA.IsTrigger == false && bodyB.IsTrigger == false)
                            {
                                bodyA.OnCollision?.Invoke(bodyB.GameObject);
                                bodyB.OnCollision?.Invoke(bodyA.GameObject);
                            }
                            else if (bodyA.IsTrigger || bodyB.IsTrigger)
                            {
                                bodyA.OnTrigger?.Invoke(bodyB.GameObject);
                                bodyB.OnTrigger?.Invoke(bodyA.GameObject);
                            }
                        }
                    }
                }
            }
        }
        
        /*
        private void ResolveCollision(Body bodyA, Body bodyB)
        {
            if (bodyA.GameObject.CompareTag("Floor") ||  bodyB.GameObject.CompareTag("Floor"))
            {
                var relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

                var e = Math.Min(bodyA.Restitution, bodyB.Restitution);

                var impulse = -(1f + e) * relativeVelocity;
                impulse /= bodyA.InvMass + bodyB.InvMass;

                bodyA.LinearVelocity.Y -= impulse.Y * bodyA.InvMass;
                bodyB.LinearVelocity.Y += impulse.Y * bodyB.InvMass;
            }

            if (bodyB.GameObject.CompareTag("Wall"))
            {
                bodyA.RestoredLastPositionInX();
            }
            else if (bodyA.GameObject.CompareTag("Wall"))
            {
                bodyB.RestoredLastPositionInX();
            }
        }*/
        
        private static bool Collide(Body bodyA, Body bodyB)
        {
            var typeCollisionA = bodyA.TypeCollision;
            var typeCollisionB = bodyB.TypeCollision;

            switch (typeCollisionA)
            {
                case TypeCollision.Box:
                    switch (typeCollisionB)
                    {
                        case TypeCollision.Box:
                            return CollisionUtilities.BoxCollider(bodyA.Transform.Position, bodyA.Size,
                                bodyB.Transform.Position, bodyB.Size);
                            
                        case TypeCollision.Circle:
                        {
                            return CollisionUtilities.IsBoxWithCircleColliding(bodyA.Transform.Position, bodyA.Size,
                                bodyB.Transform.Position, bodyB.Radius);
                        }
                    }
                    break;
                case TypeCollision.Circle:
                    switch (typeCollisionB)
                    {
                        case TypeCollision.Circle:
                            return CollisionUtilities.CircleCollider(bodyA.Transform.Position, bodyA.Radius, bodyB.Transform.Position,
                                bodyB.Radius);
                        case TypeCollision.Box:
                            return CollisionUtilities.IsBoxWithCircleColliding(bodyB.Transform.Position, bodyB.Size,
                                bodyA.Transform.Position, bodyA.Radius);
                    }
                    break;
            }
            return false;
        }
    }
}
