using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CreditScene : IScene
    {
        public Scene ID => Scene.credit;

        private Texture textureCredit;

        private Button button;
        

        public CreditScene()
        {

        }

        public void Initialize()
        {
            Texture buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            Texture buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            Texture buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            Texture buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            button = new Button(ButtonID.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540));
            button.Selected(() => GameManager.Instance.ChangeScene(Scene.menu));

            textureCredit = new Texture("Texture/Background_Menus/DefeatScreen.png");
        }

        public void Update()
        {
            
        }

        public void Render()
        {
            Renderer.Draw(textureCredit, new Transform());
        }

        public void Finish()
        {

        }
    }
}
