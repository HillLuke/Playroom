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
        public EInputType ToggleInputType;

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
                _UIManager.ActionCloseAllUI += () => { _isActive = false; ToggleActive(); };
            }

            WaitForSingletons();
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

        protected void ToggleActive()
        {
            _isActive = !_isActive;
            gameObject.SetActive(_isActive);

            if (_isActive)
            {
                OnOpen();
            }
            else
            {
                OnClose();
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

            _isActive = !ShowByDefault;
            ToggleActive();
        }

        protected virtual void KeyPressed(InputAction input)
        {
            if (input.InputType == ToggleInputType)
            {
                ToggleActive();
                Debug.Log($"Toggle on {nameof(this.gameObject)}");
            }
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }
    }
}