using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class DefeatScene : IScene
    {
        public Scene ID => Scene.defeat;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private Texture texture;

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

        public DefeatScene()
        {
            
        }

        public void Finish()
        {

        }

        public void Initialize()
        {
            LevelTextures();
            ButtonsInicialize();
        }

        public void Render()
        {
            Renderer.Draw(texture, new Transform());
        }

        public void Update()
        {
            Buttons();
        }
        public void LevelTextures()
        {
            texture = new Texture("Texture/Background_Level/Background.png");
        }
        
        private void SelectedButton()
        {
            switch (buttons[indexButton].buttonID)
            {
                case ButtonID.Restart:
                    GameManager.Instance.ChangeScene(Scene.level);
                    break;
                case ButtonID.BackToMenu:
                    GameManager.Instance.ChangeScene(Scene.menuTest);
                    break;
                case ButtonID.Exit:
                    GameManager.Instance.ExitGame();
                    break;
            }
        }
        public void ButtonsInicialize()
        {
            Texture backToMenuTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");

            Texture backToMenuTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");

            buttons.Add(new Button(ButtonID.Restart, backToMenuTextureUnSelect, backToMenuTextureSelect,
                new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 380)));
            buttons.Add(new Button(ButtonID.BackToMenu, backToMenuTextureUnSelect, backToMenuTextureSelect,
                new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 540)));
            buttons.Add(new Button(ButtonID.Exit, backToMenuTextureUnSelect, backToMenuTextureSelect,
                new Vector2(960 - (backToMenuTextureUnSelect.Width / 2), 700)));

            IndexButton = 0;
        }
        
        public void Buttons()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && indexButton > 0 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton -= 1;
            }

            if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && indexButton < buttons.Count - 1 && currentInputDelayTime > INPUT_DELAY)
            {
                currentInputDelayTime = 0;
                IndexButton += 1;
            }

            buttons[indexButton].Selected(() => SelectedButton());
        }

    }
}
