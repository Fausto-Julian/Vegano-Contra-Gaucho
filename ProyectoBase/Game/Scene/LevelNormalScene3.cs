using System.Collections.Generic;
using Game.Components;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelNormalScene3 : IScene
    {
        public SceneId Id => SceneId.LevelNormal3;

        private Player _player;

        private float _currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private readonly Texture _textureLevel;
        private readonly Texture _texturePause;
        private readonly Renderer _renderer;

        private Boss _boss;
        
        private bool _playerWin;
        
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

        public LevelNormalScene3() 
        {
            _textureLevel = new Texture("Texture/Background_Level/BackgroundNormal.png");
            _texturePause = new Texture("Texture/Background_Level/BackgroundNormalPause.png");
            _renderer = new Renderer(_textureLevel);
        }

        public void Initialize()
        {
            _renderer.Texture = _textureLevel;
            
            ButtonsInitialize();
            
            PlayerInitialize();
            
            BossInitialize();
            
            GameManager.Instance.PlayMusic("Audio/LevelNormal3.wav");
        }

        public void Update()
        {
            GamePause();
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }
        
        private void Finish()
        {
            GameManager.Instance.StopMusic();
            GameManager.Instance.ChangeScene(_playerWin ? SceneId.Victory : SceneId.Defeat);
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
                new Button(ButtonId.BackToMainMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect, new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 540)),
                new Button(ButtonId.Exit, buttonExitTextureUnSelect, buttonExitTextureSelect, new Vector2(960 - (buttonExitTextureUnSelect.Width / 2), 700))
            };

            IndexButton = 0;
            _currentInputDelayTime = 0;

            for (var i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].SetActive(false);
            }

        }

        private void PlayerInitialize()
        {
            _player = Factory.Instance.CreatePlayer();
            _player.GetComponent<HealthController>().OnDeath += OnPlayerDeathHandler;
        }

        private void OnPlayerDeathHandler()
        {
            _playerWin = false;
            Finish();
        }

        private void BossInitialize()
        {
            _boss = Factory.Instance.CreateEnemyBoss();
            _boss.GetComponent<HealthController>().OnDeath += OnBossDeathHandler;
        }

        private void OnBossDeathHandler()
        {
            _playerWin = true;
            Finish();
        }
    }
}
