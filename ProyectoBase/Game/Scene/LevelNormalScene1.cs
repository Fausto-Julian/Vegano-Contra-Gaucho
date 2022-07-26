using System;
using System.Collections.Generic;
using System.Media;
using Game.Components;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Scene
{
    public class LevelNormalScene1 : IScene
    {
        public SceneId Id => SceneId.LevelNormal1;

        private Player _player;

        private readonly Texture _textureLevel;
        private readonly Texture _texturePause;
        private readonly Renderer _renderer;
        
        private int _enemyCont;
        private bool _playerWin;

        private float _timeSpawnEnemy;
        private float _delayEnemySpawn;
        
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

        public LevelNormalScene1()
        {
            // Background level
            _textureLevel = new Texture("Texture/Background_Level/BackgroundNormal.png");
            _texturePause = new Texture("Texture/Background_Level/BackgroundNormalPause.png");
            _renderer = new Renderer(_textureLevel);
        }

        public void Initialize()
        {
            _renderer.Texture = _textureLevel;
            
            ButtonsInitialize();

            // Instance player
            _player = Factory.Instance.CreatePlayer();
            _player.GetComponent<HealthController>().OnDeath += OnPlayerDeathHandler;

            _enemyCont = 10;

            _timeSpawnEnemy = 0;
            _delayEnemySpawn = 4;
            
            GameManager.Instance.PlayMusic("Audio/LevelNormal1.wav");
        }

        public void Update()
        {
            GamePause();

            _timeSpawnEnemy += Program.DeltaTime;

            if (_timeSpawnEnemy >= _delayEnemySpawn)
            {
                var enemy = Factory.Instance.CreateEnemyBasic(false);
                
                var random = new Random();
                var numberRandom = random.Next(0, 100);

                var startPos = numberRandom <= 50 ? new Vector2(35, 200) : new Vector2(1700, 200);
                
                enemy.Initialize(startPos);
                enemy.OnDeath += OnEnemyDeathHandler;
                _timeSpawnEnemy = 0;
            }
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }

        private void Finish()
        {
            GameManager.Instance.StopMusic();
            GameManager.Instance.ChangeScene(_playerWin ? SceneId.LevelNormal2 : SceneId.Defeat);
        }

        private void OnPlayerDeathHandler()
        {
            _playerWin = false;
            Finish();
        }
        
        private void OnEnemyDeathHandler(EnemyBasic enemyBasic)
        {
            enemyBasic.OnDeath -= OnEnemyDeathHandler;
            _enemyCont--;
            if (_enemyCont <= 0)
            {
                _playerWin = true;
                Finish();
            }
        }
        
        private void GamePause()
        {

            if (Input.GetKeyDown(Keys.ESCAPE))
            {
                if (Program.ScaleTime == 1)
                {
                    _renderer.Texture = _texturePause;

                    for (var i = 0; i < _buttons.Count; i++)
                    {
                        _buttons[i].SetActive(true);
                    }

                    GameManager.Instance.SetGamePause(0);
                }
                else
                {
                    _renderer.Texture = _textureLevel;

                    for (var i = 0; i < _buttons.Count; i++)
                    {
                        _buttons[i].SetActive(false);
                    }

                    GameManager.Instance.SetGamePause(1);
                }
            }

            if (_buttons[_indexButton].IsActive)
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

            for (var i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].SetActive(false);
            }
            _buttons[_indexButton].Selected();
        }
    }
}
