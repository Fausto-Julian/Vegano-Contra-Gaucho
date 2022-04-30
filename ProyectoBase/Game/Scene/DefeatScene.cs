using System.Collections.Generic;
using Game.Component;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class DefeatScene : IScene
    {
        public Interface.Scene Id => Interface.Scene.Defeat;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private readonly Renderer renderer;

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
            renderer = new Renderer(new Texture("Texture/Background_Menus/DefeatScreen.png"));
        }

        public void Initialize()
        {
            ButtonsInitialize();
        }

        public void Render()
        {
            renderer.Draw(new Transform());
        }

        public void Update()
        {
            Buttons();
        }

        private void ButtonsInitialize()
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
            buttons[indexButton].Selected();
        }

        private void Buttons()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton -= 1;
                buttons[indexButton].Selected();
            }

            if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton += 1;
                buttons[indexButton].Selected();
            }
        }
    }
}
