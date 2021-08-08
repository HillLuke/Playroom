using Assets.Scripts.Character;
using Assets.Scripts.Singletons;
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
            CharacterInventory = GetComponent<CharacterInventory>();
            CharacterInteractor = GetComponent<PlayerInteractor>();

            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }
        }

        private void Update()
        {
            if (_inputManager != null)
            {
                if (_inputManager.LeftClick)
                {
                    Animator.SetTrigger("RightHandLeftClick");
                }

                if (_inputManager.DropFirstItem)
                {
                    CharacterInventory.DropFirstItem();
                }
            }
        }
    }
}