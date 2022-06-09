namespace Game
{
    public class Key
    {
        public Keys KeyCode { get; }
        private bool _keyDown;
        private bool _keyUp;
        private bool _firstEntryDown;

        public Key(Keys keyCode)
        {
            KeyCode = keyCode;
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