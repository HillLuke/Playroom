using Assets.Scripts.Interactable;
using Assets.Scripts.Singletons;
using System;
using UnityEngine;
using static Assets.Scripts.Interactable.InteractableBase;

namespace Assets.Scripts.Character
{
    public class PlayerInteractor : MonoBehaviour
    {
        public GameObject InteractPointFrom;
        public float Range;

        public event Action<EInteractType> Interact;

        private UnityEngine.Camera _camera;
        private GameObject _player;
        private InputManager _inputManager;
        private UIManager _uIManager;
        private InteractableBase _lookingAt;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _player = GameObject.FindGameObjectWithTag("Player");

            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }

            if (UIManager.instanceExists)
            {
                _uIManager = UIManager.instance;
            }
        }

        private void FixedUpdate()
        {
            InteractPointFrom.transform.rotation = _camera.transform.rotation;

            Ray rayFromCamera = new Ray(_camera.transform.position, _camera.transform.forward);
            RaycastHit raycastHitCamera;
            Physics.Raycast(rayFromCamera, out raycastHitCamera, 20f);

            Ray ray = new Ray(InteractPointFrom.transform.position, InteractPointFrom.transform.forward);
            RaycastHit raycastHit;

            if (Physics.Raycast(rayFromCamera, out raycastHit, Range))
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * 20f, Color.red);

                Vector3 fromPosition = InteractPointFrom.transform.position;
                Vector3 toPosition = raycastHitCamera.point;
                Vector3 direction = toPosition - fromPosition;
                Debug.DrawRay(InteractPointFrom.transform.position, direction * raycastHit.distance, Color.blue);

                var templookingAt = raycastHit.collider.gameObject.GetComponentInParent<InteractableBase>();

                if (templookingAt != _lookingAt)
                {
                    if (_lookingAt != null)
                    {
                        _lookingAt.LookAway(_player);
                        _lookingAt = null;
                    }

                    _lookingAt = templookingAt;
                }
                else
                {
                    if (_uIManager != null)
                    {
                        _uIManager.InteractText.text = string.Empty;
                    }
                }

                if (_lookingAt != null)
                {
                    _lookingAt.LookAt(_player);
                }

                if (_lookingAt != null && _inputManager.Interact)
                {
                    if (Interact != null)
                    {
                        Interact.Invoke(_lookingAt.InteractType);
                    }

                    _lookingAt.Interact(_player);
                }
            }
            else
            {
                if (_lookingAt != null)
                {
                    _lookingAt.LookAway(_player);
                    _lookingAt = null;
                }
                else
                {
                    if (_uIManager != null)
                    {
                        _uIManager.InteractText.text = string.Empty;
                    }
                }
            }
        }

        private void Start()
        {
        }
    }
}