using System;
using System.Collections;

namespace Game
{
    public static class Input
    {
        private static bool _keyDown;
        private static bool _firstEntryDown;
        private static bool _firstEntryUp;
        private static bool _keyUp;

        public static bool GetKeyDown(Keys keyCode)
        {
            if (GetKey(keyCode))
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
            return _keyDown;
        }

        public static bool GetKey(Keys keyCode)
        {
            return Engine.GetKey(keyCode);
        }
        
        public static bool GetKeyUp(Keys keyCode)
        {
            if (GetKey(keyCode))
            {
                _firstEntryUp = true;
            }
            else
            {
                if (_keyUp == false && _firstEntryUp)
                {
                    _keyUp = true;
                    _firstEntryDown = false;
                }
                else if (_keyUp)
                {
                    _keyUp = false;
                }
            }
            return _keyUp;
        }

        public static void Update()
        {
            
        }
    }
}