using System.Collections.Generic;
using Game.Component;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelScene3 : IScene
    {
        public Interface.Scene Id => Interface.Scene.Level3;
        
        public Player Player { get; private set; }

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private readonly Texture textureLevel;
        private readonly Texture texturePause;
        private readonly Renderer renderer;

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
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            renderer = new Renderer(textureLevel);
        }

        public void Initialize()
        {
            renderer.Texture = textureLevel;
            
            ButtonsInitialize();
            
            BossInitialize();

            PlayerInitialize();
        }

        public void Update()
        {
            GamePause();
        }

        public void Render()
        {
            renderer.Draw(new Transform());
        }
        
        private void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Interface.Scene.Victory : Interface.Scene.Defeat);
        }
        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 1 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                renderer.Texture = texturePause;

                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                renderer.Texture = textureLevel;

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
                    buttons[indexButton].Selected();
                }

                if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton += 1;
                    buttons[indexButton].Selected();
                }
            }
        }

        private void ButtonsInitialize() 
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

        private void PlayerInitialize()
        {
            Player = Factory.Instance.CreatePlayer();
            Player.HealthController.OnDeath += PlayerDeathHandler;
        }

        private void PlayerDeathHandler()
        {
            playerWin = false;
            Finish();
        }

        private void BossInitialize()
        {
            Boss = Factory.Instance.CreateEnemyBoss();
            Boss.HealthController.OnDeath += BossDeathHandler;
        }

        private void BossDeathHandler()
        {
            playerWin = true;
            Finish();
        }
    }
}
