using System;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    [Serializable]
    public struct InputButtonTest
    {
        private InputButtonType ButtonType;
        private KeyCode KeyCode;

        public InputButtonTest(InputButtonType buttonType, KeyCode keyCode)
        {
            ButtonType = buttonType;
            KeyCode = keyCode;
        }
    }

    [Serializable]
    public enum InputButtonType
    {
        Use,
        USe1,
        asd,
        asdas
    }
}