using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LevelScene2 : IScene
    {
        public Scene ID => Scene.level2;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;

        private List<Button> buttons = new List<Button>();
        private int indexButton;

        private bool playerWin;
        private bool bossKill;
        
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
        public Boss boss { get; private set; }
        public Player player { get; private set; }
        public LevelScene2() 
        {

        }
        public void Finish()
        {
            if (playerWin)
            {
                GameManager.Instance.ChangeScene(Scene.victory);
            }
            else
            {
                GameManager.Instance.ChangeScene(Scene.defeat);
            }
        }
        
        public void Initialize()
        {
            BossInicializate();
            ButtonsInicialize();

            PlayerInicializate();

            LevelTextures();
       
        }

        public void Render()
        {
            Renderer.Draw(currentTexture, new Transform());
        }

        public void Update()
        {
            if (bossKill)
            {
                playerWin = true;
            }
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

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = texturePause;

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
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
            Texture backToMenuTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");

            Texture backToMenuTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");

            buttons.Add(new Button(ButtonID.BackToMenu, backToMenuTextureUnSelect, backToMenuTextureSelect, new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, backToMenuTextureUnSelect, backToMenuTextureSelect, new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 700)));
            IndexButton = 0;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }
     

        public void PlayerInicializate() 
        {
            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"Texture/Player/Idle/playerIdleAnim_{i}.png"));
            }

            Animation playerAnimation = new Animation("Idle", true, 0.2f, playerIdleAnimation);
            player = new Player("Player", 100f, 250, playerAnimation, new Vector2(200, 500), Vector2.One, -90);
        }
        public void BossInicializate() 
        {
            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"Texture/Player/Idle/playerIdleAnim_{i}.png"));
            }

            Animation playerAnimation = new Animation("Idle", true, 0.2f, playerIdleAnimation);
            boss = new Boss("Boss", 325, 350, playerAnimation, new Vector2(600, 50), new Vector2(2f, 2f));
            
        }

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
