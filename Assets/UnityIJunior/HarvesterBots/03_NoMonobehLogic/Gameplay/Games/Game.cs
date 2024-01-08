using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.AssetProviders;
using _03_NoMonobehLogic.Gameplay.Factories;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Games
{
    public class Game
    {
        private readonly Transform _basePosition;
        private readonly List<Transform> _botsTransforms;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly Factory _factory = new();
        private readonly AssetProvider _assetProvider;

        public Game(
            GameObject spawnerPrefab,
            GameObject scannerPrefab,
            GameObject basePrefab,
            GameObject botPrefab,
            GameObject resourcePrefab,
            GameObject userInterfacePrefab,
            GameObject infoPanelPrefab,
            GameObject goldTextPrefab,
            GameObject resourceTextPrefab,
            GameObject sellResourceButton,
            Transform basePosition,
            List<Transform> botsTransforms,
            MonoBehaviour coroutineRunner)
        {
            _basePosition = basePosition;
            _botsTransforms = botsTransforms;
            _coroutineRunner = coroutineRunner;

            _assetProvider = new(
                spawnerPrefab,
                scannerPrefab,
                basePrefab,
                botPrefab,
                resourcePrefab,
                userInterfacePrefab,
                infoPanelPrefab,
                goldTextPrefab,
                resourceTextPrefab,
                sellResourceButton);
        }

        public void Play()
        {
            _factory.CreateSpawner(_assetProvider, _coroutineRunner);
            _factory.CreateScanner(_assetProvider, _coroutineRunner);
            _factory.CreateBots(_assetProvider, _botsTransforms, _coroutineRunner);
            _factory.CreateBase(_assetProvider, _basePosition);
            _factory.CreateUserInterface(_assetProvider);
        }

        public void Update()
        {
            _factory.BotBase.Update();
        }
    }
}