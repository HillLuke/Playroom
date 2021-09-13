using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Action<PlayerController> ActionPlayerChanged;
        public Action<PlayerController> ActionPlayerDeath;

        public PlayerController Player { get { return _player; } }

        [SerializeField]
        private PlayerController _playerPrefab;

        [SerializeField]
        private GameObject _spawnLocaion;

        private PlayerController _player;

        protected override void Awake()
        {
            if (_playerPrefab == null)
            {
                Debug.LogError("Player has not been set.");
            }

            Setup();
            base.Awake();
        }

        protected override void Setup()
        {
            SpawnPlayer();

            base.Setup();
        }

        private void SpawnPlayer()
        {
            if (_player == null)
            {
                _player = Instantiate(_playerPrefab, _spawnLocaion.transform.position, _spawnLocaion.transform.rotation);

                if (ActionPlayerChanged != null)
                {
                    ActionPlayerChanged.Invoke(Player);
                }

                _player.ActionDeath += PlayerDeath;
            }
        }

        private void PlayerDeath(PlayerController player)
        {
            if (ActionPlayerDeath != null)
            {
                ActionPlayerDeath.Invoke(_player);
            }
            _player.gameObject.SetActive(false);
            Destroy(_player.gameObject);
            _player = null;
            SpawnPlayer();
        }
    }
}