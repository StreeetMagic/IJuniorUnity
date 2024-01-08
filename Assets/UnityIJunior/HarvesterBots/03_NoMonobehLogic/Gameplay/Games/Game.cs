using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Factories;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Games
{
    public class Game
    {
        private readonly GameObject _spawnerPrefab;
        private readonly GameObject _scannerPrefab;
        private readonly GameObject _basePrefab;
        private readonly GameObject _botPrefab;
        private readonly GameObject _resourcePrefab;
        private readonly GameObject _userInterfacePrefab;
        private readonly GameObject _infoPanelPrefab;
        private readonly GameObject _goldTextPrefab;
        private readonly GameObject _resourceTextPrefab;
        private readonly GameObject _sellResourceButton;

        private readonly Transform _basePosition;
        private readonly List<Transform> _botsTransforms;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly Factory _factory = new();

        public Game(
            GameObject spawnerPrefab,
            GameObject scannerPrefab,
            GameObject basePrefab,
            GameObject botPrefab,
            GameObject resourcePrefab,
            GameObject userInterfacePrefab,
            Transform basePosition,
            List<Transform> botsTransforms,
            MonoBehaviour coroutineRunner,
            GameObject infoPanelPrefab,
            GameObject goldTextPrefab,
            GameObject resourceTextPrefab,
            GameObject sellResourceButton)
        {
            _spawnerPrefab = spawnerPrefab;
            _scannerPrefab = scannerPrefab;
            _basePrefab = basePrefab;
            _botPrefab = botPrefab;
            _resourcePrefab = resourcePrefab;
            _userInterfacePrefab = userInterfacePrefab;
            _basePosition = basePosition;
            _botsTransforms = botsTransforms;
            _coroutineRunner = coroutineRunner;
            _infoPanelPrefab = infoPanelPrefab;
            _goldTextPrefab = goldTextPrefab;
            _resourceTextPrefab = resourceTextPrefab;
            _sellResourceButton = sellResourceButton;
        }

        public void Play()
        {
            _factory.CreateSpawner(_spawnerPrefab, _resourcePrefab, _coroutineRunner);
            _factory.CreateScanner(_scannerPrefab, _coroutineRunner);
            _factory.CreateBots(_botPrefab, _botsTransforms, _coroutineRunner);
            _factory.CreateBase(_basePrefab, _basePosition);
            _factory.CreateUserInterface(_userInterfacePrefab, _infoPanelPrefab, _goldTextPrefab, _resourceTextPrefab, _sellResourceButton);
        }

        public void Update()
        {
            _factory.BotBase.Update();
        }
    }
}