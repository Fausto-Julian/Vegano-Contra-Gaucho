using System.Collections.Generic;

namespace Game.Components
{
    public class Animation
    {
        public string Id { get; }
        private readonly bool _isLoopEnabled;
        private readonly float _timeNextFrame;
        private readonly Texture[] _frames;
        private float _animationTime;
        private int _index = 0;

        public Texture CurrentFrame => _frames[_index];

        private Animation(string id, bool isLoopEnabled, float timeNextFrame, Texture[] animation)
        {
            Id = id;
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

                if (_index >= _frames.Length)
                {
                    if (_isLoopEnabled)
                    {
                        _index = 0;
                    }
                    else
                    {
                        _index = _frames.Length - 1;
                    }
                }
            }
        }

        public static Animation CreateAnimation(string id, string path, int countFrames, bool isLoopEnable, float speed)
        {
            var textures = new Texture[countFrames];

            for (var i = 0; i < countFrames; i++)
            {
                textures[i] = new Texture($"{path}{i}.png");
            }

            return new Animation(id, isLoopEnable, speed, textures);
        }

        public static Animation[] CreateAnimationRightAndLeft(string pathRight, string pathLeft, int countFrames, bool isLoopEnable, float speed)
        {
            var animations = new Animation[2];
            
            var texturesRight = new Texture[countFrames];
            var texturesLeft = new Texture[countFrames];

            for (var i = 0; i < countFrames; i++)
            {
                texturesRight[i] = new Texture($"{pathRight}{i}.png");
                texturesLeft[i] = new Texture($"{pathLeft}{i}.png");
            }

            animations[0] = new Animation("Right", isLoopEnable, speed, texturesRight);
            animations[1] = new Animation("Left", isLoopEnable, speed, texturesLeft);

            return animations;
        }

    }
}
