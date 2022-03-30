using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Transform
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; } = 0;

        public Vector2 Scale { get; set; } = Vector2.One;

    }
}
