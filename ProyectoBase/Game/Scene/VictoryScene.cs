using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class VictoryScene : IScene
    {
        public Scene ID => Scene.defeat;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture texture;

        private List<Button> buttons;
        private int indexButton;

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

        public VictoryScene()
        {

        }

        public void Finish()
        {

        }

        public void Initialize()
        {
            LevelTextures();
            ButtonsInicialize();
        }

        public void Render()
        {
            Renderer.Draw(texture, new Transform());
        }

        public void Update()
        {
            Buttons();
        }
        public void LevelTextures()
        {
            texture = new Texture("Texture/Background_Menus/VictoryScreen.png");
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
        public void ButtonsInicialize()
        {
            Texture buttonRetryTextureUnSelect = new Texture("Texture/Button/ButtonRetryUnSelected.png");
            Texture buttonRetryTextureSelect = new Texture("Texture/Button/ButtonRetrySelected.png");

            Texture buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            Texture buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            Texture buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            Texture buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons = new List<Button>();

            buttons.Add(new Button(ButtonID.Restart, buttonRetryTextureUnSelect, buttonRetryTextureSelect,
                new Vector2(960 - (buttonRetryTextureUnSelect.Width / 2), 380)));
            buttons.Add(new Button(ButtonID.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect,
                new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700)));

            IndexButton = 0;
            currentInputDelayTime = 0;
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

    }
}

