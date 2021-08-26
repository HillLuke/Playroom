using Assets.Scripts.Character;
using Assets.Scripts.Singletons;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Feeds player input in to player components
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public Animator Animator;

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
                CharacterInteractor.Interact += CharacterInteractor_Interact;
            }
        }

        private void CharacterInteractor_Interact(Interactable.InteractableBase.EInteractType interactType)
        {
            switch (interactType)
            {
                case Interactable.InteractableBase.EInteractType.Pickup:
                    Animator.SetTrigger(AnimationParameters.ACTION_PICKUP);
                    break;

                case Interactable.InteractableBase.EInteractType.Activate:
                    Animator.SetTrigger(AnimationParameters.ACTION_ACTIVATE);
                    break;
            }
        }

        private void Update()
        {
            //Todo - move this in to some other controller/function to handle animation logic
            if (_inputManager != null)
            {
                if (_inputManager.LeftClick)
                {
                    //Animator.SetTrigger("RightHandLeftClick");
                }

                if (_inputManager.DropFirstItem)
                {
                    CharacterInventory.DropFirstItem();
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