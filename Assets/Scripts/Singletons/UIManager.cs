using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using System;
using TMPro;

namespace Assets.Scripts.Singletons
{
    public class UIManager : Singleton<UIManager>
    {
        public Action ActionCloseAllUI;

        public Action<PlayerController> ActionPlayerChanged;
        public Action<InputAction> ActionKeyPressed;

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