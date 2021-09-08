using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIBase : MonoBehaviour, IPointerClickHandler
    {
        public bool ShowByDefault = false;
        public EInputType ToggleInputType;

        protected UIManager _UIManager;
        protected PlayerController _activePlayer;
        protected bool _isActive = false;

        protected virtual void Start()
        {
            _isActive = ShowByDefault;

            if (UIManager.instanceExists)
            {
                _UIManager = UIManager.instance;
                _UIManager.ActionPlayerChanged += SetPlayer;
                _UIManager.ActionCloseAllUI += () => { _isActive = false; SetActive(); };
                SetPlayer(_UIManager.GetActivePlayer());
            }
        }

        protected virtual void Awake() {}

        protected void SetActive()
        {
            gameObject.SetActive(_isActive);

            _isActive = !_isActive;
        }

        public virtual void OnPointerClick(PointerEventData eventData) {}

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
            _UIManager.ActionKeyPressed += KeyPressed;

            _isActive = ShowByDefault;
            SetActive();
        }

        protected virtual void KeyPressed(InputAction input)
        {
            if (input.InputType == ToggleInputType)
            {
                SetActive();
            }
        }
    }
}