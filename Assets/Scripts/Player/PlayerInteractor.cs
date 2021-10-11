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
        private InteractableBase _interactingWith;
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
                _uIManager.ActionCloseAllUI += () =>
                {
                    StopInteracting();
                };
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

                if (_interactingWith != null && _lookingAt != _interactingWith)
                {
                    _interactingWith.StopInteract();
                    _interactingWith = null;
                }

                _interactingWith = _lookingAt;

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

        private void Update()
        {
            if (_interactingWith != null)
            {
                var direction = _interactingWith.transform.position - _interactPointFrom.transform.position;
                var ray = new Ray(_interactPointFrom.transform.position, direction);

                RaycastHit PlayerInRangeOfInteractable;
                Physics.Raycast(ray, out PlayerInRangeOfInteractable, _range, LayerMask);

                Debug.DrawRay(_interactPointFrom.transform.position, direction, Color.red);
                Debug.DrawLine(_interactPointFrom.transform.position, PlayerInRangeOfInteractable.point, Color.green);

                if (PlayerInRangeOfInteractable.collider == null)
                {
                    StopInteracting();
                }
            }

            if (_lookingAt != null)
            {
                _uIManager.SetInteract(_lookingAt.InteractUIMessage);
            }
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
                isLookingAtInteractable = true;
            }
        }

        private void StopInteracting()
        {
            if (_interactingWith != null)
            {
                _interactingWith.StopInteract();
                _interactingWith = null;
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