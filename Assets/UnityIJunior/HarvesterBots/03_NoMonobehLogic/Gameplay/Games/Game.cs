using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.Gameplay.Bots;
using _03_NoMonobehLogic.Gameplay.Scanners;
using _03_NoMonobehLogic.Gameplay.Spawners;
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
        private readonly Transform _basePosition;
        private readonly List<Transform> _botsTransforms;
        private readonly MonoBehaviour _coroutineRunner;
        
        private Scanner _scanner;
        private Base _base;
        private List<Bot> _bots;

        public Game(
            GameObject spawnerPrefab,
            GameObject scannerPrefab,
            GameObject basePrefab,
            GameObject botPrefab,
            GameObject resourcePrefab,
            GameObject userInterfacePrefab,
            Transform basePosition,
            List<Transform> botsTransforms,
            MonoBehaviour coroutineRunner)
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
        }

        public void Launch()
        {
            LaunchSpawner();
            LaunchScanner();
            LaunchBots();
            LaunchBase();
        }

        public void Update()
        {
        }

        private void LaunchSpawner()
        {
            GameObject spawnerGameObject = Object.Instantiate(_spawnerPrefab);
            var spawner = new Spawner(spawnerGameObject, _resourcePrefab);
            spawner.Spawn(_coroutineRunner);
        }

        private void LaunchScanner()
        {
            GameObject scannerGameObject = Object.Instantiate(_scannerPrefab);
            _scanner = new Scanner(scannerGameObject);
            _scanner.Launch(_coroutineRunner);
        }

        private void LaunchBots()
        {
            _bots = new List<Bot>();

            for (int i = 0; i < _botsTransforms.Count; i++)
            {
                GameObject botGameObject = Object.Instantiate(_botPrefab);
                botGameObject.transform.parent = _botsTransforms[i];
                
                var bot = new Bot(botGameObject, _coroutineRunner);
            }
    }

        private void LaunchBase()
        {
            GameObject baseGameObject = Object.Instantiate(_basePrefab);
            _base = new Base(new Scanner(baseGameObject), new List<Bot>());
        }
    }
}