using System.Collections.Generic;

namespace Game
{
    public class Animation
    {
        public string id { get; private set; }
        private bool isLoopEnabled;
        private float timeNextFrame;
        private List<Texture> frames = new List<Texture>();
        private float animationTime;
        private int index = 0;

        public Texture currentFrame => frames[index];

        public Animation(string ID, bool IsLoopEnabled, float timeNextFrame, List<Texture> Animation)
        {
            id = ID;
            isLoopEnabled = IsLoopEnabled;
            this.timeNextFrame = timeNextFrame;
            frames = Animation;
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

        public static Animation CreateAnimation(string path, int countFrames, string id, bool isLoopEnable, float speed)
        {
            List<Texture> textures = new List<Texture>();

            for (int i = 0; i <= countFrames; i++)
            {
                textures.Add(new Texture($"{path}{i}.png"));
            }

            return new Animation(id, isLoopEnable, speed, textures);
        }

    }
}
