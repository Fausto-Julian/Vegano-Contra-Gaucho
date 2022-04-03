using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class DefeatScene : IScene
    {
        public Scene ID => Scene.defeat;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;

        private List<Button> buttons = new List<Button>();
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
        public DefeatScene()
        {
            Initialize();
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
            Renderer.Draw(currentTexture, new Transform());
        }

        public void Update()
        {
            GamePause();
        }
        public void LevelTextures()
        {
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            currentTexture = textureLevel;
        }
        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = texturePause;

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = textureLevel;


                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(false);
                }


                GameManager.Instance.SetGamePause(1);
            }

            if (buttons[indexButton].IsActive)
            {
                if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton -= 1;
                }

                if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton += 1;
                }

                buttons[indexButton].Selected(() => SelectedButton());
            }
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
            List<Texture> backToMenuTextureUnSelect = new List<Texture>();
            backToMenuTextureUnSelect.Add(new Texture("Texture/Button/ButtonStartUnSelected.png"));
            Animation backToMenuAnimationUnSelect = new Animation("UnSelected", true, 1f, backToMenuTextureUnSelect);

            List<Texture> backToMenuTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("Texture/Button/ButtonStartSelected.png"));
            Animation backToMenuAnimationSelect = new Animation("UnSelected", true, 1f, backToMenuTextureSelect);

            List<Texture> RestartTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("Texture/Button/ButtonStartSelected.png"));
            Animation RestartAnimationSelect = new Animation("UnSelected", true, 1f, RestartTextureSelect);

            buttons.Add(new Button(ButtonID.BackToMenu, backToMenuAnimationUnSelect, backToMenuAnimationSelect,
                new Vector2(960 - (backToMenuAnimationSelect.currentFrame.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, backToMenuAnimationUnSelect, backToMenuAnimationSelect,
                new Vector2(960 - (backToMenuAnimationSelect.currentFrame.Width / 2), 700)));
            buttons.Add(new Button(ButtonID.BackToMenu, RestartAnimationSelect, RestartAnimationSelect,
                new Vector2(960 - (RestartAnimationSelect.currentFrame.Width / 2), 380)));

            IndexButton = 0;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }
        // Todo: Problemita con los botones abajito
        public void Buttons()
        {
            if (buttons[indexButton].IsActive)
            {
                if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton -= 1;
                }

                if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton += 1;
                }

                buttons[indexButton].Selected(() => SelectedButton());
            }
        }

    }
}
