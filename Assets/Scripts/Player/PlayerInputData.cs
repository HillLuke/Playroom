
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputData", menuName = "PlayerInputData/New")]
    public class PlayerInputData : ScriptableObject
    {
        [Header("Actions")]
        public KeyCode Use = KeyCode.E;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Run = KeyCode.LeftShift;
        public KeyCode DropItem = KeyCode.Q;

        [Header("Sensitivity")]
        public float HorizontalSensitivity = 500f;
        public float VerticalSensitivity = 100f;

        [Header("UI")]
        public KeyCode Inventory = KeyCode.I;
        public KeyCode CloseAllUI = KeyCode.Escape;
        public KeyCode Character = KeyCode.C;
        public KeyCode ShowMouse = KeyCode.Tab;

        [Header("Camera")]
        public string YAxis = "Mouse Y";
        public string XAxis = "Mouse X";
    }

     

    public enum EInputType
    {
        Use,
        Jump,
        Run,
        DropItem,
        Inventory,
        Character,
        CloseAllUI,
        ShowMouse
    }

    public class Button
    {
        string Name;
        EInputType InputType;
        KeyCode KeyCode;
    }
}