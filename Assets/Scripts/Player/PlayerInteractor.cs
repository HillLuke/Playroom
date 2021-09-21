using Assets.Scripts.Interactable;
using Assets.Scripts.Singletons;
using System;
using UnityEngine;
using static Assets.Scripts.Interactable.InteractableBase;

namespace Assets.Scripts.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public bool DebugDraw;
        public LayerMask LayerMask;

        public event Action<EInteractType> ActionInteract;

        [SerializeField]
        private GameObject _interactPointFrom;

        [SerializeField]
        private float _range;

        private UnityEngine.Camera _camera;
        private PlayerController _player;
        private InteractableBase _lookingAt;
        private PlayerManager _playerManger;
        private InputManager _inputManager;
        private UIManager _uIManager;
        private RaycastHit raycastHitCamera;
        private Ray rayFromCamera;
        private bool isLookingAtInteractable;

        private void Start()
        {
            if (PlayerManager.instanceExists)
            {
                _playerManger = PlayerManager.instance;

                _playerManger.ActionPlayerChanged -= Setup;
                _playerManger.ActionPlayerChanged += Setup;
                Setup(_playerManger.Player);
            }

            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += Interact;
            }

            if (UIManager.instanceExists)
            {
                _uIManager = UIManager.instance;
            }
        }

        private void Interact(InputAction inputAction)
        {
            if (inputAction.InputType != EInputType.Action_Use)
            {
                return;
            }

            if (_lookingAt != null)
            {
                Debug.Log($"Interact : {_lookingAt.InteractUIMessage}");

                if (ActionInteract != null)
                {
                    ActionInteract.Invoke(_lookingAt.InteractType);
                }

                _lookingAt.Interact(_player.gameObject);
            }
        }

        private void Setup(PlayerController player)
        {
            _player = player;
            _camera = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
            rayFromCamera = new Ray(_camera.transform.position, _camera.transform.forward);
            _interactPointFrom.transform.rotation = _camera.transform.rotation;

            Physics.Raycast(rayFromCamera, out raycastHitCamera, 20f, LayerMask);
            float distance = Vector3.Distance(raycastHitCamera.point, _interactPointFrom.transform.position);

            var templookingAt = raycastHitCamera.collider?.gameObject?.GetComponentInParent<InteractableBase>();

            if ((templookingAt == null || distance > _range) && isLookingAtInteractable)
            {
                if (_lookingAt != null)
                {
                    Debug.Log($"Stopped looking at {_lookingAt.InteractUIMessage}");
                    _uIManager.SetInteract(string.Empty);
                }

                _uIManager.SetInteract(string.Empty);
                _lookingAt = null;
                isLookingAtInteractable = false;
            }
            else if (templookingAt != null && templookingAt != _lookingAt && distance <= _range)
            {
                _lookingAt = templookingAt;
                Debug.Log($"Looking at {_lookingAt.InteractUIMessage}");
                _uIManager.SetInteract(_lookingAt.InteractUIMessage);
                isLookingAtInteractable = true;
            }
        }

        private void OnDrawGizmos()
        {
            if (DebugDraw)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(raycastHitCamera.point, 0.2f);
                Gizmos.color = Color.black;
                if (_lookingAt != null)
                {
                    Gizmos.DrawSphere(raycastHitCamera.point, 0.2f);
                }

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_interactPointFrom.transform.position, _range);
            }
        }
    }
}