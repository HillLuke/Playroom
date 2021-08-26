using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerInputData", menuName = "PlayerInputData/New")]
    public class PlayerInputData : ScriptableObject
    {
        [Header("Actions")]
        public KeyCode Use = KeyCode.E;

        public KeyCode Inventory = KeyCode.I;
        public KeyCode Character = KeyCode.C;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Run = KeyCode.LeftShift;
        public KeyCode ShowMouse = KeyCode.Tab;
        public KeyCode DropItem = KeyCode.Q;

        [Header("Sensitivity")]
        public float HorizontalSensitivity = 500f;

        public float VerticalSensitivity = 6f;


        [Header("Camera")]
        public string YAxis = "Mouse Y";
        public string XAxis = "Mouse X";


    }
}