using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LevelScene3 : IScene
    {
        public Scene Id => Scene.Level3;
        
        public Player Player { get; private set; }

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;

        private Boss Boss { get; set; }
        
        private bool playerWin;
        
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

        public LevelScene3() 
        {

        }

        public void Initialize()
        {
            BossInicializate();
            ButtonsInicialize();

            PlayerInicializate();

            LevelTextures();
       
        }

        public void Update()
        {
            GamePause();
        }

        public void Render()
        {
            Renderer.Draw(currentTexture, new Transform());
        }
        
        private void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Scene.Victory : Scene.Defeat);
        }
        
        private void LevelTextures() 
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

                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = textureLevel;

                for (var i = 0; i < buttons.Count; i++)
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

                buttons[indexButton].Selected(SelectedButton);
            }
        }

        private void SelectedButton()
        {
            switch (buttons[indexButton].ButtonId)
            {
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
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            buttons = new List<Button>
            {
                new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect, new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700))
            };

            IndexButton = 0;
            currentInputDelayTime = 0;

            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }

        private void PlayerInicializate() 
        {
            Player = new Player("Player", 100f, 250, new Vector2(200, 860), Vector2.One);
            Player.HealthController.OnDeath += PlayerDeathHandler;
        }

        private void PlayerDeathHandler()
        {
            playerWin = false;
            Finish();
        }

        private void BossInicializate() 
        {
            var bossTexture = new Texture($"Texture/Boss.png");
            Boss = new Boss("Boss", 325, 350, 1.5f, bossTexture, new Vector2(600, 150));
            Boss.HealthController.OnDeath += BossDeathHandler;
        }

        private void BossDeathHandler()
        {
            playerWin = true;
            Finish();
        }
    }
}
