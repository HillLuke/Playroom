using Assets.Scripts.Utilities;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class UIManager : Singleton<UIManager>
    {
        public Action ActionInventory;

        public TextMeshProUGUI InteractText;

        private InputManager _inputManager;

        protected override void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += KeyPressed;
            }

            base.Start();
        }

        private void KeyPressed(KeyCode key)
        {
            if (key == _inputManager.PlayerInputData.Inventory)
            {
                Inventory();
            }
        }

        public void Inventory()
        {
            if (ActionInventory != null)
            {
                ActionInventory.Invoke();
            }
        }
    }
}