using Game.Component;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class CreditScene : IScene
    {
        public Interface.SceneId Id => Interface.SceneId.Credit;

        private Renderer renderer;

        private Button button;

        public CreditScene()
        {
            renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundCredits.png"));
        }

        public void Initialize()
        {
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            button = new Button(ButtonId.BackToMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 580));
            button.Selected();
        }

        public void Update()
        {
            
        }

        public void Render()
        {
            renderer.Draw(new Transform());
        }
    }
}
