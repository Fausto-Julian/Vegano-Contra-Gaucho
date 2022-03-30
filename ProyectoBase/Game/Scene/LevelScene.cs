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

        private Button backToMenu;

        public Player player { get; private set; }

        public LevelScene()
        {
            Initialize();
        }

        public void Initialize()
        {
            List<Texture> backToMenuTextureUnSelect = new List<Texture>();
            backToMenuTextureUnSelect.Add(new Texture("fence.png"));
            Animation backToMenuAnimationUnSelect = new Animation("UnSelected", true, 1f, backToMenuTextureUnSelect);
            
            List<Texture> backToMenuTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("fence.png"));
            Animation backToMenuAnimationSelect = new Animation("UnSelected", true, 1f, backToMenuTextureSelect);


            backToMenu = new Button(Scene.menu, $"ButtonBackToMenu{ID}", backToMenuAnimationUnSelect, backToMenuAnimationSelect, new Vector2(960, 540));

            backToMenu.SetActive(false);

            textureLevel = new Texture("space.png");
            texturePause = new Texture("playerIdleAnim_0.png");
            currentTexture = textureLevel;

            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"playerIdleAnim_{i}.png"));
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
            Engine.Draw(currentTexture, 0, 0, 4, 4);
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
                backToMenu.SetActive(true);
                backToMenu.Selected();
                GameManager.Instance.SetGamePause(true);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && GameManager.Instance.IsGamePause && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = textureLevel;
                backToMenu.SetActive(false);
                GameManager.Instance.SetGamePause(false);
            }
        }
    }
}
