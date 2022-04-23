using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Renderer
    {
        public static void Draw(Texture texture, Transform transform)
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Rotation);
        }
    }
}
