using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Games;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Bootstraps
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnerPrefab;
        [SerializeField] private GameObject _scannerPrefab;
        [SerializeField] private GameObject _basePrefab;
        [SerializeField] private GameObject _botPrefab;
        [SerializeField] private GameObject _resourcePrefab;
        [SerializeField] private GameObject _userInterfacePrefab;
        [SerializeField] private GameObject _infoPanelPrefab;
        [SerializeField] private GameObject _goldTextPrefab;
        [SerializeField] private GameObject _resourceTextPrefab;
        [SerializeField] private GameObject _sellResourceButton;

        [SerializeField] private Transform _basePosition;
        [SerializeField] private List<Transform> _botsTransforms;

        private Game _game;

        private void Awake()
        {
            _game = new Game(
                _spawnerPrefab,
                _scannerPrefab,
                _basePrefab,
                _botPrefab,
                _resourcePrefab,
                _userInterfacePrefab,
                _basePosition,
                _botsTransforms,
                this,
                _infoPanelPrefab,
                _goldTextPrefab,
                _resourceTextPrefab,
                _sellResourceButton);

            _game.Play();
        }

        private void Update()
        {
            _game.Update();
        }
    }
}