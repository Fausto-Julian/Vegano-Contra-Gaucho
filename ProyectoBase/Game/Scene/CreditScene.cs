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

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureCredit;

        private List<Button> buttons => new List<Button>();
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

        public CreditScene()
        {

        }

        public void Initialize()
        {
            Texture buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            Texture buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            Texture buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            Texture buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons.Add(new Button(ButtonID.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect,
                new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700)));
        }

        public void Update()
        {
            Buttons();
        }

        public void Render()
        {

        }

        public void Finish()
        {

        }
        public void Buttons()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton -= 1;
            }

            if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton += 1;
            }

            buttons[indexButton].Selected(() => SelectedButton());
        }

        private void SelectedButton()
        {
            switch (buttons[indexButton].buttonID)
            {
                case ButtonID.Restart:
                    GameManager.Instance.ChangeScene(Scene.level);
                    break;
                case ButtonID.BackToMenu:
                    GameManager.Instance.ChangeScene(Scene.menu);
                    break;
                case ButtonID.Exit:
                    GameManager.Instance.ExitGame();
                    break;
            }
        }
    }
}
