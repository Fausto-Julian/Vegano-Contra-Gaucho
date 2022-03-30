using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class LevelScene : IScene
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

        public LevelScene()
        {
            Initialize();
        }

        public void Initialize()
        {
            List<Texture> backToMenuTextureUnSelect = new List<Texture>();
            backToMenuTextureUnSelect.Add(new Texture("Texture/Button/ButtonStartUnSelected.png"));
            Animation backToMenuAnimationUnSelect = new Animation("UnSelected", true, 1f, backToMenuTextureUnSelect);
            
            List<Texture> backToMenuTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("Texture/Button/ButtonStartSelected.png"));
            Animation backToMenuAnimationSelect = new Animation("UnSelected", true, 1f, backToMenuTextureSelect);

            buttons.Add(new Button(ButtonID.BackToMenu, backToMenuAnimationUnSelect, backToMenuAnimationSelect, new Vector2(960, 540)));
            buttons.Add(new Button(ButtonID.Exit, backToMenuAnimationUnSelect, backToMenuAnimationSelect, new Vector2(960, 600)));
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

            var enemy = new EnemyTest("Enemy", 40, playerAnimation, new Vector2(600, 200));
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
                for (int i = 0; i < GameObjectManager.ActiveGameObjects.Count; i++)
                {
                    GameObjectManager.RemoveGameObject(GameObjectManager.ActiveGameObjects[i]);
                }
                GameManager.Instance.ChangeScene(Scene.level);
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
            currentInputDelayTime += Program.deltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && !GameManager.Instance.IsGamePause && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = texturePause;

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(true);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && GameManager.Instance.IsGamePause && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = textureLevel;


                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(false);
                }


                GameManager.Instance.SetGamePause(false);
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
                    Console.WriteLine("pepe");
                    break;
                case ButtonID.Exit:
                    Console.WriteLine("sali");
                    break;
            }
        }
    }
}
