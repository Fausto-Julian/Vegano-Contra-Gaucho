using System;
using System.Collections.Generic;

namespace Game
{
    public class LevelScene : IScene
    {
        public Scene ID => Scene.level;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;
        private int enemysCont;
        private bool playerWin;

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

        public Player player { get; private set; }
        public EnemyTest enemy { get; private set; }

        public LevelScene()
        {
            
        }

        public void Initialize()
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

            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            currentTexture = textureLevel;

            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"Texture/Player/Idle/playerIdleAnim_{i}.png"));
            }

            Animation playerAnimation = new Animation("Idle", true, 0.2f, playerIdleAnimation);
            player = new Player("Player", 100f, 250, playerAnimation, new Vector2(200, 500), Vector2.One, -90);

            enemy = new EnemyTest("Enemy", 40, playerAnimation, new Vector2(600, 200));
            enemysCont += 1;
            enemy.healthController.OnDeath += EliminateEnemyHandler;
        }

        public void Update()
        {
            GamePause();
        }

        public void Render()
        {
            Engine.Draw(currentTexture);
        }

        public void Finish()
        {
            if (playerWin)
            {
                GameManager.Instance.ChangeScene(Scene.level2);
            }
            else
            {
                GameManager.Instance.ChangeScene(Scene.defeat);
            }
        }

        private void EliminateEnemyHandler()
        {
            enemysCont -= 1;

            if (enemysCont <= 0)
            {
                playerWin = true;
                Finish();
            }
        }

        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 1 && currentInputDelayTime > INPUT_DELAY)
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
    }
}
