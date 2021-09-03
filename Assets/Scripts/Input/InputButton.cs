using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InputButtonT
{
    [Serializable]
    public struct InputButtonTest
    {
        InputButtonType ButtonType;
        KeyCode KeyCode;

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
