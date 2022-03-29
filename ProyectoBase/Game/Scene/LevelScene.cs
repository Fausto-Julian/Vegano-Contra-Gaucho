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
            List<Texture> backToMenuTexture = new List<Texture>();
            backToMenuTexture.Add(new Texture("playerIdleAnim_0"));
            backToMenuTexture.Add(new Texture("playerIdleAnim_1"));
            Animation backToMenuAnimation = new Animation("UnSelected", false, 0.02f, backToMenuTexture);
            backToMenu = new Button(Scene.menu, $"ButtonBackToMenu{ID}", backToMenuAnimation, backToMenuAnimation, new Vector2(960, 540));

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
            if (Engine.GetKey(Keys.ESCAPE) && !GameManager.Instance.IsGamePause)
            {
                currentTexture = texturePause;
                backToMenu.SetActive(true);
                GameManager.Instance.SetGamePause(true);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && GameManager.Instance.IsGamePause)
            {
                currentTexture = textureLevel;
                backToMenu.SetActive(false);
                GameManager.Instance.SetGamePause(false);
            }




        }
    }
}
