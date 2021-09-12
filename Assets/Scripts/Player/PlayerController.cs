using Assets.Scripts.Character;
using Assets.Scripts.Singletons;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Feeds player input in to player components
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public Action<PlayerController> ActionReady;
        public Action<PlayerController> ActionDeath;

        public Animator Animator;

        public bool IsReady { get { return _isReady; } }

        [ReadOnly]
        public CharacterEquipment CharacterEquipment;

        [ReadOnly]
        public CharacterInventory CharacterInventory;

        [ReadOnly]
        public PlayerInteractor CharacterInteractor;

        [ReadOnly]
        public CharacterStats CharacterStats;

        private InputManager _inputManager;

        private bool _wasJumping;
        private bool _isFalling;
        private bool _isReady;

        public void Death()
        {
            if (ActionDeath != null)
            {
                ActionDeath.Invoke(this);
            }
        }

        public void Spawn()
        {
            Animator.SetTrigger(AnimationParameters.SPAWN);
        }

        private void Start()
        {
            CharacterEquipment = GetComponent<CharacterEquipment>();

            if (CharacterEquipment == null)
            {
                CharacterEquipment = GetComponentInChildren<CharacterEquipment>();
            }

            CharacterInventory = GetComponent<CharacterInventory>();
            CharacterInteractor = GetComponent<PlayerInteractor>();
            CharacterStats = GetComponent<CharacterStats>();

            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }

            if (CharacterInteractor != null)
            {
                CharacterInteractor.ActionInteract += CharacterInteractor_Interact;
            }

            _isReady = true;
            if (ActionReady != null)
            {
                ActionReady.Invoke(this);
            }
        }

        private void CharacterInteractor_Interact(Interactable.InteractableBase.EInteractType interactType)
        {
            switch (interactType)
            {
                case Interactable.InteractableBase.EInteractType.Pickup:
                    Animator.ResetTrigger(AnimationParameters.ACTION_PICKUP);
                    Animator.SetTrigger(AnimationParameters.ACTION_PICKUP);
                    break;

                case Interactable.InteractableBase.EInteractType.Activate:
                    Animator.ResetTrigger(AnimationParameters.ACTION_ACTIVATE);
                    Animator.SetTrigger(AnimationParameters.ACTION_ACTIVATE);
                    break;
            }
        }

        private void Update()
        {
            //Todo - move this in to some other controller/function to handle animation logic
            if (_inputManager != null)
            {
                Animator.SetFloat(AnimationParameters.MOVEMENT_MOUSE_X, _inputManager.CameraVector.x);
                Animator.SetFloat(AnimationParameters.MOVEMENT_MOUSE_Y, _inputManager.CameraVector.y);

                if (_inputManager.CameraVector.x != 0)
                {
                    Animator.SetBool(AnimationParameters.MOVEMENT_MOUSE_TURNING, true);
                }
                else
                {
                    Animator.SetBool(AnimationParameters.MOVEMENT_MOUSE_TURNING, false);
                }

                if (_inputManager.MovementVector.magnitude > 0f)
                {
                    Animator.SetBool(AnimationParameters.MOVING, true);
                    Animator.SetFloat(AnimationParameters.MOVEMENT_VELOCITY_X, _inputManager.MovementVector.x);
                    Animator.SetFloat(AnimationParameters.MOVEMENT_VELOCITY_Z, _inputManager.MovementVector.y);
                }
                else
                {
                    Animator.SetBool(AnimationParameters.MOVING, false);
                    Animator.SetFloat(AnimationParameters.MOVEMENT_VELOCITY_X, 0f);
                    Animator.SetFloat(AnimationParameters.MOVEMENT_VELOCITY_Z, 0f);
                }

                if (_inputManager.Run)
                {
                    Animator.SetBool(AnimationParameters.SPRINT, true);
                }
                else
                {
                    Animator.SetBool(AnimationParameters.SPRINT, false);
                }

                if (CharacterStats != null)
                {
                    if (_inputManager.Jump)
                    {
                        Animator.SetTrigger(AnimationParameters.JUMP);
                    }

                    if (_wasJumping && !CharacterStats.IsJumping)
                    {
                        Animator.SetTrigger(AnimationParameters.LAND);
                    }

                    if (!CharacterStats.IsGrounded && !_wasJumping && !CharacterStats.IsJumping)
                    {
                        _isFalling = true;
                        Animator.SetBool(AnimationParameters.FALLING, _isFalling);
                    }
                    else
                    {
                        _isFalling = false;
                        Animator.SetBool(AnimationParameters.FALLING, _isFalling);
                    }
                }

                _wasJumping = CharacterStats.IsJumping;
            }
        }
    }
}