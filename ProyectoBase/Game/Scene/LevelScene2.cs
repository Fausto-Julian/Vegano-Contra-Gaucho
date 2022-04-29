﻿using System;
using System.Collections.Generic;
using Game.Component;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelScene2 : IScene
    {
        public Interface.Scene Id => Interface.Scene.Level2;
        
        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private readonly Texture textureLevel;
        private readonly Texture texturePause;
        private readonly Renderer renderer;

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

        public LevelScene2()
        {
            // Background level
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            renderer = new Renderer(textureLevel);
        }

        public void Initialize()
        {

            player = Factory.Instance.CreatePlayer();

            playerWin = false;
            player.HealthController.OnDeath += Finish;
            
            ButtonsInitialize();

            shootController =
                new ShootController("Level", new Texture("Texture/LettuceXL.png"), 400, 30, new Vector2(0f, 1f));
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
            renderer.Draw(new Transform());
        }

        private void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Interface.Scene.Level3 : Interface.Scene.Defeat);
        }
        
        private void ShootPlayer()
        {
            currentTimingShoot += Program.DeltaTime;

            if (currentTimingShoot >= coolDownShoot)
            {
                currentTimingShoot = 0;
                var number = new Random();

                var randomActivate = (float)number.Next(0, Program.WINDOW_WIDTH);
                shootController.Shoot(new Vector2(randomActivate, -50f));
            }
        }
        
        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && currentInputDelayTime > INPUT_DELAY)
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
                    GameManager.Instance.ChangeScene(Interface.Scene.Menu);
                    break;
                case ButtonId.Exit:
                    GameManager.ExitGame();
                    break;
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
    }
}