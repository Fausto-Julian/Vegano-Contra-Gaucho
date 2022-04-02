using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LevelScene2 : IScene
    {
        public Scene ID => Scene.level2;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private Texture textureLevel;
        private Texture texturePause;
        private Texture currentTexture;

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
        public Boss boss { get; private set; }
        public Player player { get; private set; }
        public LevelScene2() 
        {
            Initialize();
        }
        public void Finish()
        {
            
        }
        
        public void Initialize()
        {
            BossInicializate();
            ButtonsInicialize();

            PlayerInicializate();

            LevelTextures();
       
        }

        public void Render()
        {
            Renderer.Draw(currentTexture, new Transform());
        }

        public void Update()
        {
            GamePause();
        }

        public void LevelTextures() 
        {
            textureLevel = new Texture("Texture/Background_Level/Background.png");
            texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            currentTexture = textureLevel;
        }
        private void GamePause()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && !GameManager.Instance.IsGamePause && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                currentTexture = texturePause;

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && GameManager.Instance.IsGamePause && currentInputDelayTime > INPUT_DELAY)
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
                    Console.WriteLine("pepe");
                    break;
                case ButtonID.Exit:
                    Console.WriteLine("sali");
                    break;
            }
        }

        public void ButtonsInicialize() 
        {
            List<Texture> backToMenuTextureUnSelect = new List<Texture>();
            backToMenuTextureUnSelect.Add(new Texture("Texture/Button/ButtonStartUnSelected.png"));
            Animation backToMenuAnimationUnSelect = new Animation("UnSelected", true, 1f, backToMenuTextureUnSelect);

            List<Texture> backToMenuTextureSelect = new List<Texture>();
            backToMenuTextureSelect.Add(new Texture("Texture/Button/ButtonStartSelected.png"));
            Animation backToMenuAnimationSelect = new Animation("UnSelected", true, 1f, backToMenuTextureSelect);

            buttons.Add(new Button(ButtonID.BackToMenu, backToMenuAnimationUnSelect, backToMenuAnimationSelect,
                new Vector2(960 - (backToMenuAnimationSelect.currentFrame.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, backToMenuAnimationUnSelect, backToMenuAnimationSelect,
                new Vector2(960 - (backToMenuAnimationSelect.currentFrame.Width / 2), 700)));
            IndexButton = 0;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }
     

        public void PlayerInicializate() 
        {
            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"Texture/Player/Idle/playerIdleAnim_{i}.png"));
            }

            Animation playerAnimation = new Animation("Idle", true, 0.2f, playerIdleAnimation);
            player = new Player("Player", 100f, 250, playerAnimation, new Vector2(200, 500), Vector2.One, -90);
        }
        public void BossInicializate() 
        {
            List<Texture> playerIdleAnimation = new List<Texture>();

            for (int i = 0; i < 3; i++)
            {
                playerIdleAnimation.Add(new Texture($"Texture/Player/Idle/playerIdleAnim_{i}.png"));
            }

            Animation playerAnimation = new Animation("Idle", true, 0.2f, playerIdleAnimation);
            boss = new Boss("Boss", 325, 350, playerAnimation, new Vector2(600, 50), Vector2.One, 90);
            
        }

        public void Buttons() 
        {
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
    }
}
