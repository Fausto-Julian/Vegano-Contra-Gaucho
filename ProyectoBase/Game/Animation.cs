using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Animation
    {
        public string id;
        private bool isLoopEnabled;
        private float speed;
        private List<Texture> frames = new List<Texture>();
        private float animationTime;
        private int index = 0;

        public Texture currentFrame => frames[index];

        public Animation(string ID, bool IsLoopEnabled,float Speed, List<Texture> Animation)
        {
            id = ID;
            isLoopEnabled = IsLoopEnabled;
            speed = Speed;
            frames = Animation;
        }


        public void Update()
        {
            animationTime += Program.deltaTime;

            if (animationTime >= speed)
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


    }
}
