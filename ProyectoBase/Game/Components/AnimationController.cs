using System.Collections.Generic;

namespace Game.Components
{
    public class AnimationController : Component
    {
        private readonly List<Animation> _animations = new List<Animation>();

        private readonly Renderer _renderer;

        public AnimationController(GameObject gameObject) 
            : base(gameObject)
        {
            _renderer = gameObject.GetComponent<Renderer>();
        }
        public AnimationController(GameObject gameObject, Animation[] animations) 
            : base(gameObject)
        {
            _renderer = gameObject.GetComponent<Renderer>();
            _animations = new List<Animation>(animations);
        }

        public void AddAnimation(Animation addAnimation)
        {
            if (!_animations.Contains(addAnimation))
            {
                _animations.Add(addAnimation);
            }
        }
        
        public void RemoveAnimation(Animation removeAnimation)
        {
            if (_animations.Contains(removeAnimation))
            {
                _animations.Add(removeAnimation);
            }
        }

        public bool ChangeAnimation(string id)
        {
            for (var i = 0; i < _animations.Count; i++)
            {
                if (_animations[i].Id == id)
                {
                    _renderer.Animation = _animations[i];
                    return true;
                }
            }
            Debug.Error($"Animation not found: Id = {id}, Object Id = {GameObject.Id}");
            return false;
        }
    }
}