using System.Collections.Generic;
using Game.Component;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class MenuScene : IScene
    {
        public Interface.Scene Id => Interface.Scene.Menu;

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

        public MenuScene()
        {
            renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundMenu.png"));
        }

        public void Initialize()
        {
            //start
            var buttonStartTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");
            var buttonStartTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");
            //credits
            var buttonCreditsTextureUnSelect = new Texture("Texture/Button/ButtonCreditsUnselected.png");
            var buttonCreditsTextureSelect = new Texture("Texture/Button/ButtonCreditsSelected.png");
            //exit
            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons = new List<Button>
            {
                new Button(ButtonId.Start, buttonStartTextureUnSelect, buttonStartTextureSelect, new Vector2(960 - (buttonStartTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Credit, buttonCreditsTextureUnSelect, buttonCreditsTextureSelect, new Vector2(960 - (buttonCreditsTextureUnSelect.Width / 2), 700)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 860))
            };

            IndexButton = 0;

            currentInputDelayTime = 0;
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
                switch (buttons[indexButton].ButtonId)
                {
                    case ButtonId.Start:
                        GameManager.Instance.ChangeScene(Interface.Scene.Level);
                        break;
                    case ButtonId.Credit:
                        GameManager.Instance.ChangeScene(Interface.Scene.Credit);
                        break;
                    case ButtonId.Exit:
                        GameManager.ExitGame();
                        break;
                }
            });

        }

        public void Render()
        {
            renderer.Draw(new Transform());
        }
    }
}
