using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public enum Scene
    {
        menu,
        level,
        level2,
        defeat,
        victory
    }

    public interface IScene
    {
        Scene ID { get; }

        void Initialize();

        void Update();

        void Render();

        void Finish();
    }
}
