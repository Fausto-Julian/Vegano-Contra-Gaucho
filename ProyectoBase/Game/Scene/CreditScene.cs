using System.Media;
using Game.Components;
using Game.Interface;
using Game.Objects;

namespace Game.Scene
{
    public class CreditScene : IScene
    {
        public SceneId Id => SceneId.Credit;

        private readonly Renderer _renderer;

        private Button _button;

        public CreditScene()
        {
            _renderer = new Renderer(new Texture("Texture/Background_Menus/BackgroundCredits.png"));
        }

        public void Initialize()
        {
            var buttonBackToMenuTextureUnSelect = new Texture("Texture/Button/ButtonBTMUnSelected.png");
            var buttonBackToMenuTextureSelect = new Texture("Texture/Button/ButtonBTMSelected.png");

            _button = new Button(ButtonId.BackToMainMenu, buttonBackToMenuTextureUnSelect, buttonBackToMenuTextureSelect,
                new Vector2(960 - (buttonBackToMenuTextureUnSelect.Width / 2), 580));
            _button.Selected();
        }

        public void Update()
        {
            
        }

        public void Render()
        {
            _renderer.Draw(new Transform());
        }
    }
}
