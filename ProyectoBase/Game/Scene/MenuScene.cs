using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MenuScene : IScene
    {
        public Scene ID => Scene.menu;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureMenu;

        private List<Button> buttons = new List<Button>();
        private int indexButton = 0;

        public int IndexButton
        {
            get => indexButton;
            set
            {
                indexButton = value;
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (i != indexButton)
                    {
                        buttons[i].UnSelected();
                    }
                }
            }
        }


        public MenuScene()
        {
            
        }

        public void Initialize()
        {
            //sstart
            Texture buttonStartTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");
            Texture buttonStartTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");
            //credits
            Texture buttonCreditsTextureUnSelect = new Texture("Texture/Button/ButtonCreditsUnselected.png");
            Texture buttonCreditsTextureSelect = new Texture("Texture/Button/ButtonCreditsSelected.png");
            //exit
            Texture buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            Texture buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons.Add(new Button(ButtonID.Start, buttonStartTextureUnSelect, buttonStartTextureSelect, new Vector2(960 - (buttonStartTextureUnSelect.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Credit, buttonCreditsTextureUnSelect, buttonCreditsTextureSelect, new Vector2(960 - (buttonCreditsTextureUnSelect.Width / 2), 700)));
            buttons.Add(new Button(ButtonID.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 860)));

            IndexButton = 0;

            textureMenu = new Texture("Texture/Background_Menus/BackgroundMenu.png");
        }

        public void Update()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.W) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton -= 1;
                
            }
            if (Engine.GetKey(Keys.S) && indexButton < buttons.Count -1 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton += 1;
               
            }

            buttons[indexButton].Selected(() =>
            {
                switch (buttons[indexButton].buttonID)
                {
                    case ButtonID.Start:
                        GameManager.Instance.ChangeScene(Scene.level);
                        break;
                    case ButtonID.Credit:
                        GameManager.Instance.ChangeScene(Scene.credit);
                        break;
                    case ButtonID.Exit:
                        GameManager.Instance.ExitGame();
                        break;
                }
            });

        }

        public void Render()
        {
            Renderer.Draw(textureMenu, new Transform());
        }

        public void Finish()
        {

        }
    }
}
