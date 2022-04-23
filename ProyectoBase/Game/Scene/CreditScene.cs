using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CreditScene : IScene
    {
        public Scene Id => Scene.Credit;

        private Texture textureCredit;

        private Button button;
        

        public CreditScene()
        {

        }

        public void Initialize()
        {
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            button = new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540));
            button.Selected(() => GameManager.Instance.ChangeScene(Scene.Menu));

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
