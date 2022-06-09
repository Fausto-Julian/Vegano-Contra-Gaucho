using Game.PhysicsEngine;

namespace Game.Objects
{
    public enum ButtonId
    {
        Start,
        Credit,
        BackToMenu,
        Exit,
        Restart
    }
    public enum ButtonState
    {
        Selected,
        UnSelected
    }
    public class Button : GameObject
    {
        private readonly ButtonId _buttonId;

        private float _currentInputDelayTime;
        private const float INPUT_DELAY = 1f;

        private ButtonState _currentState;

        private readonly Texture _textureUnSelect;
        private readonly Texture _textureSelect;

        public Button(ButtonId id, Texture textureUnSelect, Texture textureSelect, Vector2 startPosition)
            : base($"Button{id}", textureUnSelect, startPosition, Vector2.One, TypeCollision.None, false, true)
        {
            _buttonId = id;
            _textureUnSelect = textureUnSelect;
            _textureSelect = textureSelect;
        }

        public void Selected()
        {
            Renderer.Texture = _textureSelect;
            _currentState = ButtonState.Selected;
        }

        public void UnSelected()
        {
            Renderer.Texture = _textureUnSelect;
            _currentState = ButtonState.UnSelected;
        }

        public override void Update()
        {
            _currentInputDelayTime += Program.RealDeltaTime;

            if (IsActive && _currentState == ButtonState.Selected && Engine.GetKey(Keys.RETURN) && _currentInputDelayTime > INPUT_DELAY)
            {
                ButtonAction();
                _currentInputDelayTime = 0;
            }

            base.Update();
        }

        private void ButtonAction()
        {
            switch (_buttonId)
            {
                case ButtonId.Start:
                    GameManager.Instance.ChangeScene(Interface.SceneId.Level);
                    break;
                case ButtonId.Credit:
                    GameManager.Instance.ChangeScene(Interface.SceneId.Credit);
                    break;
                case ButtonId.Restart:
                    GameManager.Instance.ChangeScene(Interface.SceneId.Level);
                    break;
                case ButtonId.BackToMenu:
                    GameManager.Instance.SetGamePause(1);
                    GameManager.Instance.ChangeScene(Interface.SceneId.Menu);
                    break;
                case ButtonId.Exit:
                    GameManager.ExitGame();
                    break;
                default:
                    GameManager.Instance.ChangeScene(Interface.SceneId.Menu);
                    break;
            }
        }
    }
}
