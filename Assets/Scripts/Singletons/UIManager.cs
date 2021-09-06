using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class UIManager : Singleton<UIManager>
    {
        public Action ActionInventory;
        public Action ActionCloseAllUI;

        public Action<PlayerController> ActionPlayerChanged;

        public TextMeshProUGUI InteractText;

        private InputManager _inputManager;
        private PlayerManager _playerManger;


        public PlayerController GetActivePlayer()
        {
            return _playerManger?.Player;
        }

        protected override void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += KeyPressed;
            }

            if (PlayerManager.instanceExists)
            {
                _playerManger = PlayerManager.instance;

                _playerManger.ActionPlayerChanged += x => { ActionPlayerChanged.Invoke(x); };
            }

            base.Start();
        }

        private void KeyPressed(KeyCode key)
        {
            if (key == _inputManager.PlayerInputData.Inventory)
            {
                InvokeAction(ActionInventory);
            }

            if (key == _inputManager.PlayerInputData.CloseAllUI)
            {
                InvokeAction(ActionCloseAllUI);
            }
        }

        private void InvokeAction(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}