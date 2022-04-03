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

        private Animation AnimationUnSelected;
        private Animation AnimationSelected;

        private Action myCallback;

        public Button(ButtonID id, Animation animationUnSelected, Animation animationSelected, Vector2 startPosition)
            : base($"Button{id}", animationUnSelected, startPosition, Vector2.One)
        {
            buttonID = id;
            AnimationUnSelected = animationUnSelected;
            AnimationSelected = animationSelected;
        }

        public void Selected(Action callback)
        {
            Animation = AnimationSelected;
            currentState = ButtonState.Selected;
            myCallback = callback;
        }

        public void UnSelected()
        {
            Animation = AnimationUnSelected;
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
