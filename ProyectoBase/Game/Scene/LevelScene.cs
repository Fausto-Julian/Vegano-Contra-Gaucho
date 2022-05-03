using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Component;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelScene : IScene
    {
        public Interface.SceneId Id => Interface.SceneId.Level;

        private Player player { get; set; }
        
        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Renderer renderer;
        
        private int enemyCont;
        private bool playerWin;

        private float timeSpawnEnemy;
        private float delayEnemySpawn;
        
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

        public LevelScene()
        {
            // Background level
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            renderer = new Renderer(textureLevel);
        }

        public void Initialize()
        {
            renderer.Texture = textureLevel;
            
            ButtonsInitialize();

            // Instance player
            player = Factory.Instance.CreatePlayer();
            player.HealthController.OnDeath += PlayerDeathHandler;

            enemyCont = 10;

            timeSpawnEnemy = 0;
            delayEnemySpawn = 4;
        }

        public void Update()
        {
            GamePause();

            timeSpawnEnemy += Program.DeltaTime;

            if (timeSpawnEnemy >= delayEnemySpawn)
            {
                var enemy = Factory.Instance.CreateEnemyBasic();
                
                //Todo: Crear que aparezca random dependiendo la esquina
                enemy.Initialize(new Vector2(35, 100));
                enemy.OnDeactivate += EnemyDeathHandler;
                timeSpawnEnemy = 0;
            }
        }

        public void Render()
        {
            renderer.Draw(new Transform());
        }

        private void Finish()
        {
            GameManager.Instance.ChangeScene(playerWin ? Interface.SceneId.Level2 : Interface.SceneId.Defeat);
        }

        private void PlayerDeathHandler()
        {
            playerWin = false;
            Finish();
        }
        
        private void EnemyDeathHandler()
        {
            enemyCont--;
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
                renderer.Texture = texturePause;

                for (var i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                }

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
            buttons[indexButton].Selected();
        }
    }
}
