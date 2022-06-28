using Game.Interface;

namespace Game
{
    public class Key : IKey
    {
        public Keys KeyCode => _keyCode;
        
        private bool _keyDown;
        private bool _keyUp;
        private bool _firstEntryDown;

        private readonly Keys _keyCode;

        public Key(Keys keyCode)
        {
            _keyCode = keyCode;
        }

        public void Update()
        {
            if (Engine.GetKey(KeyCode))
            {
                if (_keyDown == false && _firstEntryDown == false)
                {
                    _keyDown = true;
                }
                else if (_keyDown && _firstEntryDown == false)
                {
                    _keyDown = false;
                    _firstEntryDown = true;
                }
            }
            else
            {
                if (_keyUp == false && _firstEntryDown)
                {
                    _keyUp = true;
                    _firstEntryDown = false;
                }
                else if (_keyUp)
                {
                    _keyUp = false;
                }
            }
        }
    
        public bool GetKeyDown()
        {
            return _keyDown;
        }

        public bool GetKeyUp()
        {
            return _keyUp;
        }
    }
}