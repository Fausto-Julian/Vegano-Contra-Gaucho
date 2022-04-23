using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public enum Scene
    {
        Menu,
        Credit,
        Level,
        Level2,
        Defeat,
        Victory
    }

    public interface IScene
    {
        Scene Id { get; }

        void Initialize();

        void Update();

        void Render();
    }
}
