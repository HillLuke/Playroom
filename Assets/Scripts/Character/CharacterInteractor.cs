using Assets.Scripts.Interactable;
using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterInteractor : MonoBehaviour
    {
        public GameObject InteractPointFrom;

        private UnityEngine.Camera _camera;
        private GameObject _player;
        private InputManager _inputManager;
        protected UIManager _uIManager;
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

        private void Update()
        {
            InteractPointFrom.transform.rotation = _camera.transform.rotation;

            Ray ray = new Ray(InteractPointFrom.transform.position, InteractPointFrom.transform.forward);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 3f))
            {
                Debug.DrawRay(InteractPointFrom.transform.position, InteractPointFrom.transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);

                var templookingAt = raycastHit.collider.gameObject.GetComponent<InteractableBase>();

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