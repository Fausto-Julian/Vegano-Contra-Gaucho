using System.Collections.Generic;

namespace Game.Component
{
    public class Animation
    {
        public Texture CurrentFrame => frames[index];
        
        private bool isLoopEnabled;
        private float timeNextFrame;
        private List<Texture> frames;
        private float animationTime;
        private int index = 0;

        private Animation(bool isLoopEnabled, float timeNextFrame, List<Texture> animation)
        {
            this.isLoopEnabled = isLoopEnabled;
            this.timeNextFrame = timeNextFrame;
            frames = animation;
        }

        public void Update()
        {
            animationTime += Program.RealDeltaTime;

            if (animationTime >= timeNextFrame)
            {
                index++;
                animationTime = 0;

                if (index >= frames.Count)
                {
                    if (isLoopEnabled)
                    {
                        index = 0;
                    }
                    else
                    {
                        index = frames.Count - 1;
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
