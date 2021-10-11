using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIBase : MonoBehaviour, IPointerClickHandler
    {
        public bool ShowByDefault = false;
        public bool ListenForCloseAll = false;
        public EInputType ToggleInputType;
        public EUIElement UIActionToListenFor;

        protected UIManager _UIManager;

        [ReadOnly]
        [ShowInInspector]
        protected PlayerController _activePlayer;

        [ReadOnly]
        [ShowInInspector]
        protected bool _isActive = false;

        protected virtual void Start()
        {
            if (UIManager.instanceExists)
            {
                _UIManager = UIManager.instance;
                _UIManager.ActionPlayerChanged += SetPlayer;
                if (ListenForCloseAll)
                {
                    _UIManager.ActionCloseAllUI += () => { OnClose(); };
                }

                if (UIActionToListenFor != EUIElement.NONE)
                {
                    _UIManager.ActionUIElementAction += UIElementActionListener;
                }
            }

            WaitForSingletons();
        }

        private void UIElementActionListener(UIElementAction uIElementAction)
        {
            if (uIElementAction.InputType == UIActionToListenFor)
            {
                if (uIElementAction.Open)
                {
                    OnOpen();
                }
                else
                {
                    OnClose();
                }
            }
        }

        private void WaitForSingletons()
        {
            if (_UIManager.isSetup)
            {
                SetPlayer(_UIManager.GetActivePlayer());
            }
            else
            {
                _UIManager.ActionSetup -= WaitForSingletons;
                _UIManager.ActionSetup += WaitForSingletons;
            }
        }


        public virtual void OnPointerClick(PointerEventData eventData)
        {
        }

        private void SetPlayer(PlayerController player)
        {
            if (player == null)
            {
                return;
            }

            if (!player.IsReady)
            {
                player.ActionReady += SetPlayer;
                return;
            }

            _activePlayer = player;
            Setup();
        }

        protected virtual void Setup()
        {
            _UIManager.ActionKeyPressed -= KeyPressed;
            _UIManager.ActionKeyPressed += KeyPressed;

            gameObject.SetActive(ShowByDefault);
        }

        protected virtual void KeyPressed(InputAction input)
        {
            if (input.InputType == ToggleInputType)
            {
                OnToggle();
                Debug.Log($"Toggle on {nameof(this.gameObject)}");
            }
        }

        protected void OnToggle()
        {
            if (!gameObject.activeSelf)
            {
                OnOpen();
            }
            else
            {
                OnClose();
            }
        }

        protected virtual void OnOpen()
        {
            _isActive = true;
            gameObject.SetActive(_isActive);
        }

        protected virtual void OnClose()
        {
            _isActive = false;
            gameObject.SetActive(_isActive);
        }
    }
}