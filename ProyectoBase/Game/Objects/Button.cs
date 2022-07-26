using Game.Interface;
using Game.PhysicsEngine;

namespace Game.Objects
{
    public enum ButtonId
    {
        Start,
        Credit,
        BackToMainMenu,
        Exit,
        Restart,
        NormalMode,
        VeganMode
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
        private const float INPUT_DELAY = 0.5f;

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

            if (IsActive && _currentState == ButtonState.Selected && Input.GetKeyUp(Keys.RETURN) && _currentInputDelayTime > INPUT_DELAY)
            {
                _currentInputDelayTime = 0;
                ButtonAction();
            }

            base.Update();
        }

        private void ButtonAction()
        {
            switch (_buttonId)
            {
                case ButtonId.Start:
                    GameManager.Instance.ChangeScene(SceneId.SelectMode);
                    break;
                case ButtonId.Credit:
                    GameManager.Instance.ChangeScene(SceneId.Credit);
                    break;
                case ButtonId.Restart:
                    GameManager.Instance.ChangeScene(SceneId.SelectMode);
                    break;
                case ButtonId.BackToMainMenu:
                    GameManager.Instance.SetGamePause(1);
                    GameManager.Instance.ChangeScene(SceneId.Menu);
                    break;
                case ButtonId.Exit:
                    GameManager.ExitGame();
                    break;
                case ButtonId.NormalMode:
                    GameManager.Instance.ModeVegan = false;
                    GameManager.Instance.ChangeScene(SceneId.LevelNormal1);
                    break;
                case ButtonId.VeganMode:
                    GameManager.Instance.ModeVegan = true;
                    GameManager.Instance.ChangeScene(SceneId.LevelVegan1);
                    break;
                default:
                    GameManager.Instance.ChangeScene(Interface.SceneId.Menu);
                    break;
            }
        }
    }
}
