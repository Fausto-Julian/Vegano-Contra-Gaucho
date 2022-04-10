using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MenuTest : IScene
    {
        public Scene ID => Scene.credit;

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
            Texture backToMenuTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");

            Texture backToMenuTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");

            button = new Button(ButtonID.Start, backToMenuTextureUnSelect, backToMenuTextureSelect, new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 540));
            button.Selected(() => GameManager.Instance.ChangeScene(Scene.level));

            textureLevel = new Texture("Texture/Background_Menus/BackgroundMenu.png");
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
