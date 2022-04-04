using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum ButtonID
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
    class Button : GameObject
    {
        public ButtonID buttonID { get; private set; }

        private float currentInputDelayTime;
        private const float INPUT_DELAY = 0.2f;

        private ButtonState currentState;

        private Texture textureUnSelect;
        private Texture textureSelect;

        private Action myCallback;

        public Button(ButtonID id, Texture textureUnSelect, Texture textureSelect, Vector2 startPosition)
            : base($"Button{id}", textureUnSelect, startPosition, Vector2.One)
        {
            buttonID = id;
            this.textureUnSelect = textureUnSelect;
            this.textureSelect = textureSelect;
        }

        public void Selected(Action callback)
        {
            texture = textureSelect;
            currentState = ButtonState.Selected;
            myCallback = callback;
        }

        public void UnSelected()
        {
            texture = textureUnSelect;
            currentState = ButtonState.UnSelected;
        }

        public override void Update()
        {
            currentInputDelayTime += Program.deltaTime;

            if (currentState == ButtonState.Selected && Engine.GetKey(Keys.SPACE) && currentInputDelayTime > INPUT_DELAY)
            {
                ButtonAction();
                currentInputDelayTime = 0;
            }

            base.Update();
        }

        public void ButtonAction()
        {
            myCallback?.Invoke();
        }

    }
}
