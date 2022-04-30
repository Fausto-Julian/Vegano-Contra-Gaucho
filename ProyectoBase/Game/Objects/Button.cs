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
        public ButtonId ButtonId { get; private set; }

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private ButtonState currentState;

        private readonly Texture textureUnSelect;
        private readonly Texture textureSelect;

        public Button(ButtonId id, Texture textureUnSelect, Texture textureSelect, Vector2 startPosition)
            : base($"Button{id}", textureUnSelect, startPosition, Vector2.One)
        {
            ButtonId = id;
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

            if (IsActive && currentState == ButtonState.Selected && Engine.GetKey(Keys.SPACE) && currentInputDelayTime > INPUT_DELAY)
            {
                ButtonAction();
                currentInputDelayTime = 0;
            }

            base.Update();
        }

        private void ButtonAction()
        {
            switch (ButtonId)
            {
                case ButtonId.Start:
                    GameManager.Instance.ChangeScene(Interface.Scene.Level);
                    break;
                case ButtonId.Credit:
                    GameManager.Instance.ChangeScene(Interface.Scene.Credit);
                    break;
                case ButtonId.Restart:
                    GameManager.Instance.ChangeScene(Interface.Scene.Level);
                    break;
                case ButtonId.BackToMenu:
                    GameManager.Instance.ChangeScene(Interface.Scene.Menu);
                    break;
                case ButtonId.Exit:
                    GameManager.ExitGame();
                    break;
                default:
                    GameManager.Instance.ChangeScene(Interface.Scene.Menu);
                    break;
            }
        }

    }
}
