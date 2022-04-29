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

        private Action myCallback;

        public Button(ButtonId id, Texture textureUnSelect, Texture textureSelect, Vector2 startPosition)
            : base($"Button{id}", textureUnSelect, startPosition, Vector2.One)
        {
            ButtonId = id;
            this.textureUnSelect = textureUnSelect;
            this.textureSelect = textureSelect;
        }

        public void Selected(Action callback)
        {
            Renderer.Texture = textureSelect;
            currentState = ButtonState.Selected;
            myCallback = callback;
        }

        public void UnSelected()
        {
            Renderer.Texture = textureUnSelect;
            currentState = ButtonState.UnSelected;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.DeltaTime;

            if (currentState == ButtonState.Selected && Engine.GetKey(Keys.SPACE) && currentInputDelayTime > INPUT_DELAY)
            {
                ButtonAction();
                currentInputDelayTime = 0;
            }

            base.Update();
        }

        private void ButtonAction()
        {
            myCallback?.Invoke();
        }

    }
}
