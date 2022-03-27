using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public enum ButtonState
    {
        Selected,
        UnSelected
    }

    class Button : GameObject
    {
        private ButtonState currentState;
        private Scene NextScene;

        private Animation AnimationUnSelected;
        private Animation AnimationSelected;

        public Button(Scene nextScene,string id, Animation animationUnSelected, Animation animationSelected, Vector2 startPosition) 
            : base(id, animationUnSelected, startPosition, Vector2.One)
        {
            NextScene = nextScene;
            AnimationUnSelected = animationUnSelected;
            AnimationSelected = animationSelected;
        }

        public void Selected()
        {
            Animation = AnimationSelected;
            currentState = ButtonState.Selected;
        }

        public void UnSelected()
        {
            Animation = AnimationUnSelected;
            currentState = ButtonState.UnSelected;
        }

        public override void Update()
        {
            if (currentState == ButtonState.Selected && Engine.GetKey(Keys.SPACE))
                GameManager.Instance.ChangeScene(NextScene);

            base.Update();
        }

    }
}
