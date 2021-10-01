using Assets.Scripts.Inventory;
using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Singletons
{
    public class UIManager : Singleton<UIManager>
    {
        public Action ActionCloseAllUI;

        public Action<PlayerController> ActionPlayerChanged;
        public Action<InputAction> ActionKeyPressed;
        public Action<string> ActionInteractor;
        public Action<ItemCollection> ActionOpenStorage;
        public Action ActionCloseStorage;

        private InputManager _inputManager;
        private PlayerManager _playerManger;

        public PlayerController GetActivePlayer()
        {
            return _playerManger?.Player;
        }

        public void SetInteract(string interactNotice)
        {
            if (ActionInteractor != null)
            {
                ActionInteractor.Invoke(interactNotice);
            }
        }

        public void OpenStorage(ItemCollection itemCollection)
        {
            if (ActionOpenStorage != null)
            {
                ActionOpenStorage.Invoke(itemCollection);
            }
        }
        public void CloseStorage()
        {
            if (ActionCloseStorage != null)
            {
                ActionCloseStorage.Invoke();
            }
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

                _playerManger.ActionPlayerChanged += PlayerChanged;
            }
            Setup();
            base.Start();
        }

        protected override void Setup()
        {
            if (_inputManager.isSetup && _playerManger.isSetup)
            {
                base.Setup();
            }
            else
            {
                _inputManager.ActionSetup += Setup;
                _playerManger.ActionSetup += Setup;
            }
        }

        private void PlayerChanged(PlayerController playerController)
        {
            if (ActionPlayerChanged != null)
            {
                ActionPlayerChanged.Invoke(playerController);
            }
        }

        private void KeyPressed(InputAction inputAction)
        {
            if (inputAction.InputType == EInputType.UI_CloseAllUI)
            {
                if (ActionCloseAllUI != null)
                {
                    ActionCloseAllUI.Invoke();
                }
            }
            else
            {
                if (ActionKeyPressed != null)
                {
                    ActionKeyPressed.Invoke(inputAction);
                }
            }
        }
    }
}