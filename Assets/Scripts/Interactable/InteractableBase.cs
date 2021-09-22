using Assets.Scripts.Singletons;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public abstract class InteractableBase : MonoBehaviour
    {
        public abstract string InteractUIMessage { get; }

        //Todo Think of a naming structure for enums.
        public EInteractType InteractType;

        public bool MaintainRange = false;

        protected UIManager _uIManager;
        protected InputManager _inputManager;

        protected virtual void Start()
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

        protected virtual void Awake()
        {
        }

        public abstract void Interact(GameObject Interactor);

        public virtual void StopInteract() { }

        public enum EInteractType
        {
            Pickup,
            Activate
        }
    }
}