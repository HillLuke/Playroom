using Assets.Scripts.Interactable;
using Assets.Scripts.Player;
using Assets.Scripts.UI.ItemCollections;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets.Scripts.Singletons
{

    public class UIManager : Singleton<UIManager>
    {
        public Action ActionCloseAllUI;

        public Action<PlayerController> ActionPlayerChanged;
        public Action<InputAction> ActionKeyPressed;
        public Action<string> ActionInteractor;
        public Action<Storage, GameObject> ActionOpenStorage;
        public Action ActionCloseStorage;
        public Action<UIElementAction> ActionUIElementAction;
        public Action<UIItemCollection> ActionInteractWithUICollection;
        public Action<UIItemCollection> ActionStopInteractWithUICollection;

        private InputManager _inputManager;
        private PlayerManager _playerManager;

        public PlayerController GetActivePlayer()
        {
            return _playerManager?.Player;
        }

        public void InteractWithUICollection(UIItemCollection uIItemCollection)
        {
            if (ActionInteractWithUICollection != null)
            {
                ActionInteractWithUICollection.Invoke(uIItemCollection);
            }
        }
        public void StopInteractWithUICollection(UIItemCollection uIItemCollection)
        {
            if (ActionStopInteractWithUICollection != null)
            {
                ActionStopInteractWithUICollection.Invoke(uIItemCollection);
            }
        }

        public void SetInteract(string interactNotice)
        {
            if (ActionInteractor != null)
            {
                ActionInteractor.Invoke(interactNotice);
            }
        }

        public void OpenStorage(Storage storage, GameObject Interactor)
        {
            if (ActionOpenStorage != null)
            {
                ActionOpenStorage.Invoke(storage, Interactor);
            }
        }

        public void CloseStorage()
        {
            if (ActionCloseStorage != null)
            {
                ActionCloseStorage.Invoke();
            }
        }
        public void RaiseUIElementAction(UIElementAction uIElementAction)
        {
            if (ActionUIElementAction != null)
            {
                ActionUIElementAction.Invoke(uIElementAction);
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
                _playerManager = PlayerManager.instance;

                _playerManager.ActionPlayerChanged += PlayerChanged;
            }
            Setup();
            base.Start();
        }

        protected override void Setup()
        {
            if (_inputManager.isSetup && _playerManager.isSetup)
            {
                base.Setup();
            }
            else
            {
                _inputManager.ActionSetup += Setup;
                _playerManager.ActionSetup += Setup;
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

    public enum EUIElement
    {
        NONE,
        Inventory
    }

    [Serializable]
    public struct UIElementAction
    {
        public EUIElement InputType;
        public bool Open;
    }
}