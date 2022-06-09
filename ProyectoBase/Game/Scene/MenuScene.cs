﻿using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class MenuScene : IScene
    {
        public SceneId Id => SceneId.Menu;

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

        public MenuScene()
        {
            _renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundMenu.png"));
        }

        public void Initialize()
        {
            //start
            var buttonStartTextureUnSelect = new Texture("Texture/Button/ButtonStartUnSelected.png");
            var buttonStartTextureSelect = new Texture("Texture/Button/ButtonStartSelected.png");
            //credits
            var buttonCreditsTextureUnSelect = new Texture("Texture/Button/ButtonCreditsUnselected.png");
            var buttonCreditsTextureSelect = new Texture("Texture/Button/ButtonCreditsSelected.png");
            //exit
            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            _buttons = new List<Button>
            {
                new Button(ButtonId.Start, buttonStartTextureUnSelect, buttonStartTextureSelect, new Vector2(960 - (buttonStartTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Credit, buttonCreditsTextureUnSelect, buttonCreditsTextureSelect, new Vector2(960 - (buttonCreditsTextureUnSelect.Width / 2), 700)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 860))
            };

            IndexButton = 0;
            _buttons[_indexButton].Selected();
            _currentInputDelayTime = 0;
        }

        public void Update()
        {
            _currentInputDelayTime += Program.RealDeltaTime;

            if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && _indexButton > 0 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                IndexButton -= 1;
                _buttons[_indexButton].Selected();
            }
            if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && _indexButton < _buttons.Count -1 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
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
