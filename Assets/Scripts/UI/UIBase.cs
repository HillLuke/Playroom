using Assets.Scripts.Singletons;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIBase : MonoBehaviour
    {
        protected UIManager _UIManager;
        public bool ShowByDefault = true;

        protected bool _isActive = false;

        protected virtual void Start()
        {
            _isActive = ShowByDefault;

            if (UIManager.instanceExists)
            {
                _UIManager = UIManager.instance;
            }

            SetActive();
        }

        protected void SetActive()
        {
            gameObject.SetActive(_isActive);

            _isActive = !_isActive;
        }
    }
}