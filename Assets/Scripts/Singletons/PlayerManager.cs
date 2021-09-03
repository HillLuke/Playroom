using Assets.Scripts.Player;
using Assets.Scripts.UI;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public Action<PlayerController> ActionPlayerChanged;

        public PlayerController Player { get { return _player; } }

        [SerializeField]
        private PlayerController _playerPrefab;
        [SerializeField]
        private GameObject _spawnLocaion;

        private PlayerController _player;

        protected override void Start()
        {
            if (_playerPrefab == null)
            {
                Debug.LogError("Player has not been set.");
            }

            SpawnPlayer();

            base.Start();
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
            }
        }
    }
}