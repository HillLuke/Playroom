using Assets.Scripts.Singletons;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public abstract class InteractableBase : MonoBehaviour
    {
        public abstract string InteractUIMessage { get; }

        protected UIManager _uIManager;
        protected InputManager _inputManager;

        protected virtual void Awake()
        {
            if (UIManager.instanceExists)
            {
                _uIManager = UIManager.instance;
            }

            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }
        }

        public abstract void Interact(GameObject Interactor);

        public virtual void LookAt(GameObject Interactor)
        {
            if (_uIManager != null)
            {
                _uIManager.InteractText.text = InteractUIMessage;
            }
        }

        public virtual void LookAway(GameObject Interactor)
        {
            if (_uIManager != null)
            {
                _uIManager.InteractText.text = string.Empty;
            }
        }
    }
}