using System;
using System.Collections.Generic;

namespace Game.Components
{
    public class Transform
    {
        
        public Vector2 Position = Vector2.Zero;

        public float Angle = 0;
        
        public Vector2 Scale = Vector2.One;
        /*
        private Vector2 _position = Vector2.One;
        
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                for (var i = 0; i < _transformsChildren.Count; i++)
                {
                    _transformsChildren[i].Position = _position;
                }
            }
        }

        private float _angle = 0;

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                for (var i = 0; i < _transformsChildren.Count; i++)
                {
                    _transformsChildren[i].Angle = _angle;
                }
            }
        }
        
        private Vector2 _scale = Vector2.One;

        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                for (var i = 0; i < _transformsChildren.Count; i++)
                {
                    _transformsChildren[i].Scale = _scale;
                }
            }
        }

        public readonly float Sin;
        public readonly float Cos;
        
        private readonly List<Transform> _transformsChildren = new List<Transform>();

        public Transform()
        {
            Sin = (float)Math.Sin(_angle);
            Cos = (float)Math.Cos(_angle);
        }

        public Transform(Vector2 position)
        {
            _position = position;
            Sin = (float)Math.Sin(_angle);
            Cos = (float)Math.Cos(_angle);
        }
        
        public Transform(Vector2 position, float angle)
        {
            _position = position;
            _angle = angle;
            Sin = (float)Math.Sin(_angle);
            Cos = (float)Math.Cos(_angle);
        }
        
        public Transform(Vector2 position, float angle, Vector2 scale)
        {
            _position = position;
            _angle = angle;
            _scale = scale;
            Sin = (float)Math.Sin(_angle);
            Cos = (float)Math.Cos(_angle);
        }

        public void SetChildren(Transform children)
        {
            _transformsChildren.Add(children);
        }

        public void SetParent(Transform parent)
        {
            parent.SetChildren(this);
        }

        public void RemoveChildren(Transform children)
        {
            if (_transformsChildren.Contains(children))
            {
                _transformsChildren.Remove(children);
            }
        }

        public void RemoveParent(Transform parent)
        {
            parent.RemoveChildren(this);
        }*/
    }
}