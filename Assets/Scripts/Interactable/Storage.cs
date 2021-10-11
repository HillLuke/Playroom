using Assets.Scripts.Camera;
using Assets.Scripts.Inventory;
using Assets.Scripts.Singletons;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class Storage : InteractableBase
    {
        public override string InteractUIMessage => $"Open ({_inputManager.PlayerInputData.Action_Use.KeyCode})";

        public SphereCollider Collider;
        public ItemCollection ItemCollection { get { return _itemCollection; } }

        private ItemCollection _itemCollection;
        private bool _isInteracting;
        private CameraRig _cmeraRig;

        protected override void Start()
        {
            if (CameraRig.instanceExists)
            {
                _cmeraRig = CameraRig.instance;
            }

            base.Start();
        }

        protected override void Awake()
        {
            _itemCollection = gameObject.GetComponent<ItemCollection>();

            if (_itemCollection == null)
            {
                Debug.LogError("ItemCollection is null");
            }

            base.Awake();
        }

        public override void Interact(GameObject Interactor)
        {
            if (!_isInteracting)
            {
                _isInteracting = true;
                _uIManager.OpenStorage(this, Interactor);
                _uIManager.RaiseUIElementAction(new UIElementAction { InputType = EUIElement.Inventory, Open = true });
                _inputManager.ShowMouse();
                _inputManager.LockControl();
                _cmeraRig.PauseCamera();
            }
        }

        public override void StopInteract()
        {
            _isInteracting = false;
            _uIManager.CloseStorage();
            _cmeraRig.ResumeCamera();
            base.StopInteract();
        }

    }
}