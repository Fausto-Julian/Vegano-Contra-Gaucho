using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Renderer
    {
        public static void Draw(Texture texture, Transform transform)
        {
            Engine.Draw(texture, transform.Position.x, transform.Position.y, transform.Scale.x, transform.Scale.y, transform.Rotation);
        }
    }
}
