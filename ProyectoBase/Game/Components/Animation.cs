using System.Collections.Generic;

namespace Game.Components
{
    public class Animation
    {
        private readonly bool _isLoopEnabled;
        private readonly float _timeNextFrame;
        private readonly List<Texture> _frames;
        private float _animationTime;
        private int _index = 0;

        public Texture CurrentFrame => _frames[_index];

        private Animation(bool isLoopEnabled, float timeNextFrame, List<Texture> animation)
        {
            _isLoopEnabled = isLoopEnabled;
            _timeNextFrame = timeNextFrame;
            _frames = animation;
        }

        public void Update()
        {
            _animationTime += Program.RealDeltaTime;

            if (_animationTime >= _timeNextFrame)
            {
                _index++;
                _animationTime = 0;

                if (_index >= _frames.Count)
                {
                    if (_isLoopEnabled)
                    {
                        _index = 0;
                    }
                    else
                    {
                        _index = _frames.Count - 1;
                    }
                }
            }
        }

        public static Animation CreateAnimation(string path, int countFrames, bool isLoopEnable, float speed)
        {
            var textures = new List<Texture>();

            for (var i = 0; i <= countFrames; i++)
            {
                textures.Add(new Texture($"{path}{i}.png"));
            }

            return new Animation(isLoopEnable, speed, textures);
        }

    }
}
