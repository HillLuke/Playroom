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

        private InputManager _inputManager;

        private void Start()
        {
            CharacterEquipment = GetComponent<CharacterEquipment>();

            if (CharacterEquipment == null)
            {
                CharacterEquipment = GetComponentInChildren<CharacterEquipment>();
            }

            CharacterInventory = GetComponent<CharacterInventory>();
            CharacterInteractor = GetComponent<PlayerInteractor>();

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

            }
        }
    }
}