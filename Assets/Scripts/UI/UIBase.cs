using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIBase : MonoBehaviour, IPointerClickHandler
    {
        public bool ShowByDefault = true;
        public bool ListenForKey;         

        protected UIManager _UIManager;
        protected bool _isActive = false;
        protected PlayerController _activePlayer;

        protected virtual void Start()
        {
            _isActive = ShowByDefault;

            if (UIManager.instanceExists)
            {
                _UIManager = UIManager.instance;
                _UIManager.ActionPlayerChanged += Setup;
                Setup(_UIManager.GetActivePlayer());
            }
        }

        protected void SetActive()
        {
            gameObject.SetActive(_isActive);

            _isActive = !_isActive;
        }

        public virtual void OnPointerClick(PointerEventData eventData) { }

        protected virtual void Setup(PlayerController player)
        {
            if (player == null)
            {
                return;
            }

            if (!player.IsReady)
            {
                player.ActionReady += Setup;
                return;
            }

            _activePlayer = player;
            SetActive();
        }
    }
}