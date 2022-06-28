using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class SelectModeScene : IScene
    {
        public SceneId Id => SceneId.SelectMode;

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

        public SelectModeScene()
        {
            _renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundSelectMode.png"));
        }

        public void Initialize()
        {
            //start
            var buttonNormalModeTextureUnSelect = new Texture("Texture/Button/ButtonNormalModeUnSelected.png");
            var buttonNormalModeTextureSelect = new Texture("Texture/Button/ButtonNormalModeSelected.png");
            //credits
            var buttonVeganModeTextureUnSelect = new Texture("Texture/Button/ButtonVeganModeUnSelected.png");
            var buttonVeganModeTextureSelect = new Texture("Texture/Button/ButtonVeganModeSelected.png");
            // Back To Main Menu Texture
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            _buttons = new List<Button>
            {
                new Button(ButtonId.NormalMode, buttonNormalModeTextureUnSelect, buttonNormalModeTextureSelect, new Vector2(960 - (buttonNormalModeTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.VeganMode, buttonVeganModeTextureUnSelect, buttonVeganModeTextureSelect, new Vector2(960 - (buttonVeganModeTextureUnSelect.Width / 2), 700)),
                new Button(ButtonId.BackToMainMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect, new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 860))
            };

            IndexButton = 0;
            _buttons[_indexButton].Selected();
        }

        public void Update()
        {
            if ((Input.GetKeyDown(Keys.W) || Input.GetKeyDown(Keys.UP)) && _indexButton > 0)
            {
                IndexButton -= 1;
                _buttons[_indexButton].Selected();
            }

            if ((Input.GetKeyDown(Keys.S) || Input.GetKeyDown(Keys.DOWN)) && _indexButton < _buttons.Count - 1)
            {
                IndexButton += 1;
                _buttons[_indexButton].Selected();
            }
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }
    }
}