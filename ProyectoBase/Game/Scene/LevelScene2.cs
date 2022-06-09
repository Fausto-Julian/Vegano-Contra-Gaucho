using System;
using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelScene2 : IScene
    {
        public SceneId Id => SceneId.Level2;
        
        private float _currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;
        
        private readonly Texture _textureLevel;
        private readonly Texture _texturePause;
        private readonly Renderer _renderer;

        private ShootController _shootController;
        
        private List<Button> _buttons;
        private int _indexButton;

        private float _currentTimingShoot;
        private float _coolDownShoot;

        private float _timeNextScene;

        private bool _playerWin;
        private Player _player;
        
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

        public LevelScene2()
        {
            // Background level
            _textureLevel = new Texture("Texture/Background_Level/Background.png");
            _texturePause = new Texture("Texture/Background_Level/BackgroundPause.png");
            _renderer = new Renderer(_textureLevel);
        }

        public void Initialize()
        {
            ButtonsInitialize();
            
            _renderer.Texture = _textureLevel;
            
            _player = Factory.Instance.CreatePlayer();

            _playerWin = false;
            _player.GetComponent<HealthController>().OnDeath += OnPlayerDeathHandler;

            _shootController =
                new ShootController("Level2", new Texture("Texture/LettuceXL.png"), 400, 30, new Vector2(0f, 1f));
            _coolDownShoot = 1;

            _timeNextScene = 60;
        }

        public void Update()
        {
            GamePause();

            ShootPlayer();

            _timeNextScene -= Program.DeltaTime;
            if (_timeNextScene <= 0)
            {
                _playerWin = true;
                Finish();
            }
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }

        private void Finish()
        {
            GameManager.Instance.ChangeScene(_playerWin ? SceneId.Level3 : SceneId.Defeat);
        }

        private void OnPlayerDeathHandler()
        {
            _playerWin = false;
            Finish();
        }
        
        private void ShootPlayer()
        {
            _currentTimingShoot += Program.DeltaTime;

            if (_currentTimingShoot >= _coolDownShoot)
            {
                _currentTimingShoot = 0;
                var number = new Random();

                var randomActivate = (float)number.Next(0, Program.WINDOW_WIDTH);
                _shootController.Shoot(new Vector2(randomActivate, -50f));
            }
        }
        
        private void GamePause()
        {
            _currentInputDelayTime += Program.RealDeltaTime;

            if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 1 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                _renderer.Texture = _texturePause;

                for (var i = 0; i < _buttons.Count; i++)
                {
                    _buttons[i].SetActive(true);
                };

                GameManager.Instance.SetGamePause(0);
            }
            else if (Engine.GetKey(Keys.ESCAPE) && Program.ScaleTime == 0 && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                _renderer.Texture = _textureLevel;

                for (var i = 0; i < _buttons.Count; i++)
                {
                    _buttons[i].SetActive(false);
                }

                GameManager.Instance.SetGamePause(1);
            }

            if (_buttons[_indexButton].IsActive)
            {
                if ((Engine.GetKey(Keys.W) || Engine.GetKey(Keys.UP)) && _indexButton > 0 && _currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton -= 1;
                    _buttons[_indexButton].Selected();
                }

                if ((Engine.GetKey(Keys.S) || Engine.GetKey(Keys.DOWN)) && _indexButton < _buttons.Count - 1 && _currentInputDelayTime > INPUT_DELAY)
                {
                    IndexButton += 1;
                    _buttons[_indexButton].Selected();
                }
            }
        }
        
        private void ButtonsInitialize() 
        {
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            var buttonExitTextureUnSelect = new Texture("Texture/Button/ButtonExitUnSelected.png");
            var buttonExitTextureSelect = new Texture("Texture/Button/ButtonExitSelected.png");

            _buttons = new List<Button>
            {
                new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect, new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700))
            };

            IndexButton = 0;
            _currentInputDelayTime = 0;

            for (var i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].SetActive(false);
            }
            _buttons[_indexButton].Selected();
        }
    }
}