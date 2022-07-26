using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class VictoryScene : IScene
    {
        public SceneId Id => SceneId.Victory;

        private readonly Renderer _renderer;
        private readonly Texture _backgroundNormal;
        private readonly Texture _backgroundVegan;

        private List<Button> _buttons;
        private int _indexButton;

        private int IndexButton
        {
            get => _indexButton;
            set
            {
                _indexButton = value;

                for (var i = 0; i < _buttons.Count; i++)
                {
                    if (i != _indexButton)
                    {
                        _buttons[i].UnSelected();
                    }
                }

            }
        }

        public VictoryScene()
        {
            _backgroundNormal = new Texture("Texture/Background_Menus/BackgroundNormalVictory.png");
            _backgroundVegan = new Texture("Texture/Background_Menus/BackgroundVeganVictory.png");
            _renderer = new Renderer(_backgroundNormal);
        }

        public void Initialize()
        {
            _renderer.Texture = GameManager.Instance.ModeVegan ? _backgroundVegan : _backgroundNormal;
            
            ButtonsInitialize();
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }

        public void Update()
        {
            Buttons();
        }

        private void ButtonsInitialize()
        {
            var buttonRetryTextureUnSelect = new Texture("Texture/Button/ButtonRetryUnSelected.png");
            var buttonRetryTextureSelect = new Texture("Texture/Button/ButtonRetrySelected.png");

            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            _buttons = new List<Button>
            {
                new Button(ButtonId.Restart, buttonRetryTextureUnSelect, buttonRetryTextureSelect,
                    new Vector2(960 - (buttonRetryTextureUnSelect.Width / 2), 380)),
                new Button(ButtonId.BackToMainMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                    new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect,
                    new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700))
            };

            IndexButton = 0;
            _buttons[_indexButton].Selected();
            
            GameManager.Instance.PlayMusic("Audio/Menu.wav");
        }

        private void Buttons()
        {
            if ((Input.GetKeyDown(Keys.W) || Input.GetKeyDown(Keys.UP)) && _indexButton > 0)
            {
                IndexButton -= 1;
                _buttons[_indexButton].Selected();
            }
            if ((Input.GetKeyDown(Keys.S) || Input.GetKeyDown(Keys.DOWN)) && _indexButton < _buttons.Count -1)
            {
                IndexButton += 1;
                _buttons[_indexButton].Selected();
            }
        }

    }
}

