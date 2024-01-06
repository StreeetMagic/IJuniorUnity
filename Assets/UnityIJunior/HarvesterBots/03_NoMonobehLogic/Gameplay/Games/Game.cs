using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.Gameplay.Bots;
using _03_NoMonobehLogic.Gameplay.Scanners;
using _03_NoMonobehLogic.Gameplay.Spawners;
using _03_NoMonobehLogic.UserInterfaces;
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
            LaunchUserInterface();
        }

        public void Update()
        {
            _base.Update();
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
                GameObject botGameObject = Object.Instantiate(_botPrefab, _botsTransforms[i].position, _botsTransforms[i].rotation, _botsTransforms[i]);
                botGameObject.name = $"Bot {i}";

                var characterController = botGameObject.GetComponent<CharacterController>();
                var colliderBehaviour = botGameObject.GetComponent<ColliderBehaviour>();

                Transform spawnPosition = _botsTransforms[i];

                var bot = new Bot(botGameObject, _coroutineRunner, spawnPosition, characterController, colliderBehaviour);

                _bots.Add(bot);
            }
        }

        private void LaunchBase()
        {
            GameObject baseGameObject = Object.Instantiate(_basePrefab, _basePosition.position, _basePosition.rotation);
            _base = new Base(_scanner, _bots, baseGameObject);
            baseGameObject.GetComponent<BaseView>().Base = _base;
        }

        private void LaunchUserInterface()
        {
            GameObject userInterfaceGameObject = Object.Instantiate(_userInterfacePrefab);
            userInterfaceGameObject.GetComponent<UserInterface>().Init(_base);
        }
    }
}