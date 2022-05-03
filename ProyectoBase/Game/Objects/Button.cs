using System;

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
        private ButtonId buttonId;

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 1f;

        private ButtonState currentState;

        private Texture textureUnSelect;
        private Texture textureSelect;

        public Button(ButtonId id, Texture textureUnSelect, Texture textureSelect, Vector2 startPosition)
            : base($"Button{id}", textureUnSelect, startPosition, Vector2.One)
        {
            buttonId = id;
            this.textureUnSelect = textureUnSelect;
            this.textureSelect = textureSelect;
        }

        public void Selected()
        {
            Renderer.Texture = textureSelect;
            currentState = ButtonState.Selected;
        }

        public void UnSelected()
        {
            Renderer.Texture = textureUnSelect;
            currentState = ButtonState.UnSelected;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.RealDeltaTime;

            if (IsActive && currentState == ButtonState.Selected && Engine.GetKey(Keys.RETURN) && currentInputDelayTime > INPUT_DELAY)
            {
                ButtonAction();
                currentInputDelayTime = 0;
            }

            base.Update();
        }

        private void ButtonAction()
        {
            switch (buttonId)
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
