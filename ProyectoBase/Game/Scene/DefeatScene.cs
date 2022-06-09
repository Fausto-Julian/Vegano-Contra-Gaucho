using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class DefeatScene : IScene
    {
        public SceneId Id => SceneId.Defeat;

        private float _currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private readonly Renderer _renderer;

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

        public DefeatScene()
        {
            _renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundDefeat.png"));
        }

        public void Initialize()
        {
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
                    new Vector2(960 - (buttonRetryTextureUnSelect.Width / 2), 400)),
                new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                    new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 560)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect,
                    new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 720))
            };

            IndexButton = 0;
            _currentInputDelayTime = 0;
            _buttons[_indexButton].Selected();
        }

        private void Buttons()
        {
            _currentInputDelayTime += Program.RealDeltaTime;

            if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && _indexButton > 0 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                IndexButton -= 1;
                _buttons[_indexButton].Selected();
            }

            if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && _indexButton < _buttons.Count - 1 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                IndexButton += 1;
                _buttons[_indexButton].Selected();
            }
        }
    }
}
