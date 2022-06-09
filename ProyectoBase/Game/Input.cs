using System.Collections.Generic;

namespace Game
{
    public static class Input
    {
        private static List<Key> _keys = new List<Key>();

        public static void Initialize()
        {
            _keys.Add(new Key(Keys.A));
            _keys.Add(new Key(Keys.B));
            _keys.Add(new Key(Keys.C));
            _keys.Add(new Key(Keys.D));
            _keys.Add(new Key(Keys.E));
            _keys.Add(new Key(Keys.F));
            _keys.Add(new Key(Keys.F1));
            _keys.Add(new Key(Keys.F2));
            _keys.Add(new Key(Keys.F3));
            _keys.Add(new Key(Keys.F4));
            _keys.Add(new Key(Keys.F5));
            _keys.Add(new Key(Keys.F6));
            _keys.Add(new Key(Keys.F7));
            _keys.Add(new Key(Keys.F8));
            _keys.Add(new Key(Keys.F9));
            _keys.Add(new Key(Keys.F10));
            _keys.Add(new Key(Keys.F11));
            _keys.Add(new Key(Keys.F12));
            _keys.Add(new Key(Keys.F13));
            _keys.Add(new Key(Keys.F14));
            _keys.Add(new Key(Keys.F15));
            _keys.Add(new Key(Keys.G));
            _keys.Add(new Key(Keys.H));
            _keys.Add(new Key(Keys.I));
            _keys.Add(new Key(Keys.J));
            _keys.Add(new Key(Keys.K));
            _keys.Add(new Key(Keys.L));
            _keys.Add(new Key(Keys.M));
            _keys.Add(new Key(Keys.N));
            _keys.Add(new Key(Keys.Num0));
            _keys.Add(new Key(Keys.Num1));
            _keys.Add(new Key(Keys.Num2));
            _keys.Add(new Key(Keys.Num3));
            _keys.Add(new Key(Keys.Num4));
            _keys.Add(new Key(Keys.Num5));
            _keys.Add(new Key(Keys.Num6));
            _keys.Add(new Key(Keys.Num7));
            _keys.Add(new Key(Keys.Num8));
            _keys.Add(new Key(Keys.Num9));
            _keys.Add(new Key(Keys.O));
            _keys.Add(new Key(Keys.P));
            _keys.Add(new Key(Keys.Q));
            _keys.Add(new Key(Keys.R));
            _keys.Add(new Key(Keys.S));
            _keys.Add(new Key(Keys.T));
            _keys.Add(new Key(Keys.U));
            _keys.Add(new Key(Keys.V));
            _keys.Add(new Key(Keys.W));
            _keys.Add(new Key(Keys.X));
            _keys.Add(new Key(Keys.Y));
            _keys.Add(new Key(Keys.Z));
            _keys.Add(new Key(Keys.AT));
            _keys.Add(new Key(Keys.AX));
            _keys.Add(new Key(Keys.UP));
            _keys.Add(new Key(Keys.ADD));
            _keys.Add(new Key(Keys.END));
            _keys.Add(new Key(Keys.OEM_102));
            _keys.Add(new Key(Keys.TAB));
            _keys.Add(new Key(Keys.YEN));
            _keys.Add(new Key(Keys.APPS));
            _keys.Add(new Key(Keys.BACK));
            _keys.Add(new Key(Keys.DOWN));
            _keys.Add(new Key(Keys.HOME));
            _keys.Add(new Key(Keys.KANA));
            _keys.Add(new Key(Keys.LEFT));
            _keys.Add(new Key(Keys.LWIN));
            _keys.Add(new Key(Keys.MAIL));
            _keys.Add(new Key(Keys.MUTE));
            _keys.Add(new Key(Keys.NEXT));
            _keys.Add(new Key(Keys.RWIN));
            _keys.Add(new Key(Keys.STOP));
            _keys.Add(new Key(Keys.WAKE));
            _keys.Add(new Key(Keys.ABNT_C1));
            _keys.Add(new Key(Keys.ABNT_C2));
            _keys.Add(new Key(Keys.COLON));
            _keys.Add(new Key(Keys.COMMA));
            _keys.Add(new Key(Keys.GRAVE));
            _keys.Add(new Key(Keys.KANJI));
            _keys.Add(new Key(Keys.LMENU));
            _keys.Add(new Key(Keys.MINUS));
            _keys.Add(new Key(Keys.PAUSE));
            _keys.Add(new Key(Keys.POWER));
            _keys.Add(new Key(Keys.PRIOR));
            _keys.Add(new Key(Keys.RIGHT));
            _keys.Add(new Key(Keys.RMENU));
            _keys.Add(new Key(Keys.SLASH));
            _keys.Add(new Key(Keys.SLEEP));
            _keys.Add(new Key(Keys.SPACE));
            _keys.Add(new Key(Keys.SYSRQ));
            _keys.Add(new Key(Keys.DELETE));
            _keys.Add(new Key(Keys.DIVIDE));
            _keys.Add(new Key(Keys.EQUALS));
            _keys.Add(new Key(Keys.ESCAPE));
            _keys.Add(new Key(Keys.INSERT));
            _keys.Add(new Key(Keys.LSHIFT));
            _keys.Add(new Key(Keys.NUMPAD0));
            _keys.Add(new Key(Keys.NUMPAD1));
            _keys.Add(new Key(Keys.NUMPAD2));
            _keys.Add(new Key(Keys.NUMPAD3));
            _keys.Add(new Key(Keys.NUMPAD4));
            _keys.Add(new Key(Keys.NUMPAD5));
            _keys.Add(new Key(Keys.NUMPAD6));
            _keys.Add(new Key(Keys.NUMPAD7));
            _keys.Add(new Key(Keys.NUMPAD8));
            _keys.Add(new Key(Keys.NUMPAD9));
            _keys.Add(new Key(Keys.PERIOD));
            _keys.Add(new Key(Keys.RETURN));
            _keys.Add(new Key(Keys.RSHIFT));
            _keys.Add(new Key(Keys.SCROLL));
            _keys.Add(new Key(Keys.CAPITAL));
            _keys.Add(new Key(Keys.CONVERT));
            _keys.Add(new Key(Keys.DECIMAL));
            _keys.Add(new Key(Keys.NUMLOCK));
            _keys.Add(new Key(Keys.WEBBACK));
            _keys.Add(new Key(Keys.WEBHOME));
            _keys.Add(new Key(Keys.WEBSTOP));
            _keys.Add(new Key(Keys.LBRACKET));
            _keys.Add(new Key(Keys.LCONTROL));
            _keys.Add(new Key(Keys.MULTIPLY));
            _keys.Add(new Key(Keys.RBRACKET));
            _keys.Add(new Key(Keys.RCONTROL));
            _keys.Add(new Key(Keys.SUBTRACT));
            _keys.Add(new Key(Keys.VOLUMEUP));
            _keys.Add(new Key(Keys.BACKSLASH));
            _keys.Add(new Key(Keys.MEDIASTOP));
            _keys.Add(new Key(Keys.NEXTTRACK));
            _keys.Add(new Key(Keys.NOCONVERT));
            _keys.Add(new Key(Keys.PLAYPAUSE));
            _keys.Add(new Key(Keys.PREVTRACK));
            _keys.Add(new Key(Keys.SEMICOLON));
            _keys.Add(new Key(Keys.UNDERLINE));
            _keys.Add(new Key(Keys.UNLABELED));
            _keys.Add(new Key(Keys.WEBSEARCH));
            _keys.Add(new Key(Keys.APOSTROPHE));
            _keys.Add(new Key(Keys.CALCULATOR));
            _keys.Add(new Key(Keys.MYCOMPUTER));
            _keys.Add(new Key(Keys.VOLUMEDOWN));
            _keys.Add(new Key(Keys.WEBFORWARD));
            _keys.Add(new Key(Keys.WEBREFRESH));
            _keys.Add(new Key(Keys.MEDIASELECT));
            _keys.Add(new Key(Keys.NUMPADCOMMA));
            _keys.Add(new Key(Keys.NUMPADENTER));
            _keys.Add(new Key(Keys.NUMPADEQUALS));
            _keys.Add(new Key(Keys.WEBFAVORITES));
            Engine.Debug("Initialize Input Completed.");
        }

        public static void Update()
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                _keys[i].Update();
            }
        }
        
        public static bool GetKeyDown(Keys keyCode)
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].KeyCode == keyCode)
                {
                    return _keys[i].GetKeyDown();
                }
            }
            return false;
        }

        public static bool GetKeyStay(Keys keyCode)
        {
            return Engine.GetKey(keyCode);
        }

        public static bool GetKeyUp(Keys keyCode)
        {
            for (var i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].KeyCode == keyCode)
                {
                    return _keys[i].GetKeyUp();
                }
            }
            return false;
        }
    }
}