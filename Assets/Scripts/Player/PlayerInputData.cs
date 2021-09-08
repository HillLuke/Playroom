using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputData", menuName = "PlayerInputData/New")]
    public class PlayerInputData : ScriptableObject
    {
        [Header("Movement")]
        public InputAction Movement_Jump = new InputAction { InputType = EInputType.Jump, KeyCode = KeyCode.Space };

        public InputAction Movement_Run = new InputAction { InputType = EInputType.Run, KeyCode = KeyCode.LeftShift };

        [Header("Actions")]
        public InputAction Action_Use = new InputAction { InputType = EInputType.Use, KeyCode = KeyCode.E };

        public InputAction Action_DropItem = new InputAction { InputType = EInputType.DropItem, KeyCode = KeyCode.Q };

        [Header("UI")]
        public InputAction UI_CloseAll = new InputAction { InputType = EInputType.CloseAllUI, KeyCode = KeyCode.Escape };

        public InputAction UI_Inventory = new InputAction { InputType = EInputType.Inventory, KeyCode = KeyCode.I };
        public InputAction UI_Character = new InputAction { InputType = EInputType.Character, KeyCode = KeyCode.C };
        public InputAction UI_ShowMouse = new InputAction { InputType = EInputType.ShowMouse, KeyCode = KeyCode.Tab };

        [Header("Camera")]
        public string YAxis = "Mouse Y";

        public string XAxis = "Mouse X";

        [Header("Sensitivity")]
        public float HorizontalSensitivity = 500f;

        public float VerticalSensitivity = 100f;
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

    [Serializable]
    public struct InputAction
    {
        public EInputType InputType;
        public KeyCode KeyCode;
    }
}