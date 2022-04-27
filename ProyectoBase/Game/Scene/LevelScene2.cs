﻿

using System;
using System.Collections.Generic;

namespace Game
{
    public class LevelScene2 : IScene
    {
        public Scene Id => Scene.Level2;
        
        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private readonly Texture backgroundTexture = new Texture("Texture/Background_Level/Background.png");
        private readonly Texture backgroundPauseTexture = new Texture("Texture/Background_Level/BackgroundPause.png");
        private Texture currentTexture;

        private ShootController shootController;
        
        private List<Button> buttons;
        private int indexButton;

        private float currentTimingShoot;
        private float coolDownShoot;

        private float timeNextScene;

        private bool playerWin;
        private Player player { get; set; }
        
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

        public void Initialize()
        {
            currentTexture = backgroundTexture;

            player = new Player("Player", 100f, 250, new Vector2(200, 860), Vector2.One);

            playerWin = false;
            player.HealthController.OnDeath += Finish;
            
            ButtonsInicialize();

            shootController =
                new ShootController("Level", "Texture/LettuceXL.png", 400, 30, new Vector2(0f, 1f), false);
            coolDownShoot = 1;

            timeNextScene = 60;
        }

        public void Update()
        {
            GamePause();

            ShootPlayer();

            timeNextScene -= Program.DeltaTime;
            if (timeNextScene <= 0)
            {
                playerWin = true;
                Finish();
            }
        }

        public void Render()
        {
            Engine.Draw(currentTexture);
        }

        private void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Scene.Level3 : Scene.Defeat);
        }
        
        private void ShootPlayer()
        {
            currentTimingShoot += Program.DeltaTime;

            if (currentTimingShoot >= coolDownShoot)
            {
                currentTimingShoot = 0;
                var number = new Random();

                var ramdomActivate = (float)number.Next(0, Program.WINDOW_WIDTH);
                shootController.Shoot(new Vector2(ramdomActivate, -50f));
            }
        }
        
        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = backgroundPauseTexture;

                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = backgroundTexture;

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

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }
    }
}