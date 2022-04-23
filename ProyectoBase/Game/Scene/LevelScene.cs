using System;
using System.Collections.Generic;

namespace Game
{
    public class LevelScene : IScene
    {
        public Scene Id => Scene.Level;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;
        private int enemyCont;
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

        private Player player;
        private EnemyTest Enemy { get; set; }

        private readonly PoolGeneric<EnemyTest> enemys = new PoolGeneric<EnemyTest>();

        private float timeSpawnEnemy;
        private float delayEnemySpawn;

        public LevelScene()
        {
            
        }

        public void Initialize()
        {
            InitializeButtons();

            // Background level
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            currentTexture = textureLevel;

            // Instance player
            player = new Player("Player", 100f, 250, new Vector2(200, 500), Vector2.One);

            var enemyTexture = new Texture("Texture/Vegan1.png");
            Enemy = new EnemyTest("Enemy", 40, enemyTexture, new Vector2(600, 400));

            enemyCont += 1;
            Enemy.HealthController.OnDeath += EliminateEnemyHandler;

            timeSpawnEnemy = 0;
            delayEnemySpawn = 10;

        }

        public void Update()
        {
            GamePause();

            timeSpawnEnemy += Program.DeltaTime;

            if (timeSpawnEnemy >= delayEnemySpawn)
            {
                SpawnEnemy();
                timeSpawnEnemy = 0;
            }
        }

        public void Render()
        {
            Engine.Draw(currentTexture);
        }

        public void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Scene.Level2 : Scene.Defeat);
        }

        private void EliminateEnemyHandler()
        {
            enemyCont -= 1;

            if (enemyCont <= 0)
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

        private void InitializeButtons()
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

        private void SpawnEnemy()
        {
            var enemy = enemys.GetorCreate();

            if (enemy.Value == null)
            {
                enemy.Value = new EnemyTest("enemy", 100, new Texture("Texture/Vegan2.png"), new Vector2(35, 100));
                enemy.Value.OnDesactive += () =>
                {
                    enemy.Value.SetActive(false);
                    enemys.AddPool(enemy);
                };
            }
            else
            {
                enemy.Value.Initialize(new Vector2(35, 200), 100);
            }
        }
    }
}
