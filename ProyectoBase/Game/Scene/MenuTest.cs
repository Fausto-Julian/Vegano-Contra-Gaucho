using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MenuTest : IScene
    {
        public Scene ID => Scene.menuTest;

        private Texture textureLevel;
        private Button button;

        public MenuTest()
        {
            
        }

        public void Finish()
        {
            
        }

        public void Initialize()
        {
            List<Texture> backToMenuTextureUnSelect = new List<Texture>();
            backToMenuTextureUnSelect.Add(new Texture("Texture/Button/ButtonStartUnSelected.png"));
            Animation backToMenuAnimationUnSelect = new Animation("UnSelected", true, 1f, backToMenuTextureUnSelect);

            List<Texture> backToMenuTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("Texture/Button/ButtonStartSelected.png"));
            Animation backToMenuAnimationSelect = new Animation("UnSelected", true, 1f, backToMenuTextureSelect);

            button = new Button(ButtonID.BackToMenu, backToMenuAnimationUnSelect, backToMenuAnimationSelect, new Vector2(960 - (backToMenuAnimationSelect.currentFrame.Width / 2), 540));
            button.Selected(() => GameManager.Instance.ChangeScene(Scene.level));

            textureLevel = new Texture("Texture/Background_Level/Background.png");
        }

        public void Render()
        {
            Renderer.Draw(textureLevel, new Transform());
        }

        public void Update()
        {
            
        }
    }
}
