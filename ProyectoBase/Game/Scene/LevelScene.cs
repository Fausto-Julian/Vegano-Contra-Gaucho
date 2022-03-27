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

        private Texture texture;
        private int enemysCont;
        private bool playerWin;

        public Player player { get; private set; }

        public LevelScene()
        {
            Initialize();
        }

        public void Initialize()
        {
            texture = new Texture("space.png");


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
            
        }

        public void Render()
        {
            Engine.Draw(texture, 0, 0, 4, 4);
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
    }
}
