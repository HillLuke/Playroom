using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIBase : MonoBehaviour, IPointerClickHandler
    {
        public bool ShowByDefault = true;
        public EInputType ToggleAction;

        protected UIManager _UIManager;
        protected bool _isActive = false;
        protected PlayerController _activePlayer;

        protected virtual void Start()
        {
            _isActive = ShowByDefault;

            if (UIManager.instanceExists)
            {
                _UIManager = UIManager.instance;
                _UIManager.ActionPlayerChanged += SetPlayer;
                SetPlayer(_UIManager.GetActivePlayer());
            }
        }

        protected virtual void Awake()
        {

        }

        protected void SetActive()
        {
            gameObject.SetActive(_isActive);

            _isActive = !_isActive;
        }

        public virtual void OnPointerClick(PointerEventData eventData) { }

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
            SetActive();
        }
    }
}