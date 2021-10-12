using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputData", menuName = "PlayerInputData/New")]
    public class PlayerInputData : ScriptableObject
    {
        [Header("Movement")]
        public InputAction Movement_Jump = new InputAction { InputType = EInputType.Movement_Jump, KeyCode = KeyCode.Space };
        public InputAction Movement_Run = new InputAction { InputType = EInputType.Movement_Run, KeyCode = KeyCode.LeftShift };

        [Header("Actions")]
        public InputAction Action_Use = new InputAction { InputType = EInputType.Action_Use, KeyCode = KeyCode.E };
        public InputAction Action_DropItem = new InputAction { InputType = EInputType.Action_DropItem, KeyCode = KeyCode.Q };

        [Header("UI")]
        public InputAction UI_CloseAll = new InputAction { InputType = EInputType.UI_CloseAllUI, KeyCode = KeyCode.Escape };
        public InputAction UI_Inventory = new InputAction { InputType = EInputType.UI_Inventory, KeyCode = KeyCode.I };
        public InputAction UI_Character = new InputAction { InputType = EInputType.UI_Character, KeyCode = KeyCode.C };
        public InputAction UI_ShowMouse = new InputAction { InputType = EInputType.UI_ShowMouse, KeyCode = KeyCode.Tab };
        public InputAction UI_Shift = new InputAction { InputType = EInputType.UI_Shift, KeyCode = KeyCode.LeftShift };

        [Header("Camera")]
        public string YAxis = "Mouse Y";
        public string XAxis = "Mouse X";

        [Header("Axis")]
        public string Horizontal = "Horizontal";
        public string Vertical = "Vertical";

        [Header("Sensitivity")]
        public float HorizontalSensitivity = 500f;
        public float VerticalSensitivity = 100f;
    }

    public enum EInputType
    {
        NONE,
        Movement_Jump,
        Movement_Run,
        Action_Use,
        Action_DropItem,
        UI_Inventory,
        UI_Character,
        UI_CloseAllUI,
        UI_ShowMouse,
        UI_Shift
    }

    [Serializable]
    public struct InputAction
    {
        public EInputType InputType;
        public KeyCode KeyCode;
    }
}