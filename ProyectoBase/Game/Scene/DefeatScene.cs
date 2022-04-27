using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class DefeatScene : IScene
    {
        public Scene Id => Scene.Defeat;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private Texture texture;

        private List<Button> buttons;
        private int indexButton;

        private int IndexButton
        {
            get => indexButton;
            set
            {
                indexButton = value;

                for (var i = 0; i < buttons.Count; i++)
                {
                    if (i != indexButton)
                    {
                        buttons[i].UnSelected();
                    }
                }

            }
        }

        public DefeatScene()
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

        private void LevelTextures()
        {
            texture = new Texture("Texture/Background_Menus/DefeatScreen.png");
        }
        
        private void SelectedButton()
        {
            switch (buttons[indexButton].ButtonId)
            {
                case ButtonId.Restart:
                    GameManager.Instance.ChangeScene(Scene.Level);
                    break;
                case ButtonId.BackToMenu:
                    GameManager.Instance.ChangeScene(Scene.Menu);
                    break;
                case ButtonId.Exit:
                    GameManager.ExitGame();
                    break;
            }
        }

        private void ButtonsInicialize()
        {
            var buttonRetryTextureUnSelect = new Texture("Texture/Button/ButtonRetryUnSelected.png");
            var buttonRetryTextureSelect = new Texture("Texture/Button/ButtonRetrySelected.png");

            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons = new List<Button>
            {
                new Button(ButtonId.Restart, buttonRetryTextureUnSelect, buttonRetryTextureSelect,
                    new Vector2(960 - (buttonRetryTextureUnSelect.Width / 2), 380)),
                new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                    new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect,
                    new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700))
            };

            IndexButton = 0;
            currentInputDelayTime = 0;
        }

        private void Buttons()
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

            buttons[indexButton].Selected(SelectedButton);
        }

    }
}
