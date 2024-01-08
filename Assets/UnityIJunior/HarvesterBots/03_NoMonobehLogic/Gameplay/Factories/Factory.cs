using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.Gameplay.Bots;
using _03_NoMonobehLogic.Gameplay.Resourcess;
using _03_NoMonobehLogic.Gameplay.Scanners;
using _03_NoMonobehLogic.Gameplay.Spawners;
using _03_NoMonobehLogic.UserInterfaces;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Factories
{
    public class Factory
    {
        private readonly Dictionary<GameObject, Resource> _resources = new();
        private readonly Dictionary<GameObject, Base> _bases = new();

        private Scanner _scanner;
        private List<Bot> _bots;

        public IReadOnlyDictionary<GameObject, Resource> Resources => _resources;
        public IReadOnlyDictionary<GameObject, Base> Bases => _bases;
        public Base BotBase { get; private set; }

        public void CreateResource(GameObject resourcePrefab, List<Vector3> spawnPoints, GameObject parent)
        {
            GameObject gameObject = Object.Instantiate(resourcePrefab, spawnPoints[Random.Range(0, spawnPoints.Count)], Quaternion.identity);
            gameObject.transform.parent = parent.transform;
            Resource resource = new(gameObject);

            _resources.Add(gameObject, resource);
        }

        public void CreateBase(GameObject basePrefab, Transform basePosition)
        {
            GameObject baseGameObject = Object.Instantiate(basePrefab, basePosition.position, basePosition.rotation);
            BotBase = new Base(_scanner, _bots, baseGameObject);

            _bases.Add(baseGameObject, BotBase);
        }

        public void CreateScanner(GameObject scannerPrefab, MonoBehaviour coroutineRunner)
        {
            GameObject scannerGameObject = Object.Instantiate(scannerPrefab);
            _scanner = new Scanner(scannerGameObject, this);
            _scanner.Launch(coroutineRunner);
        }

        public void CreateBots(GameObject botPrefab, List<Transform> botsTransforms, MonoBehaviour coroutineRunner)
        {
            _bots = new List<Bot>();

            for (int i = 0; i < botsTransforms.Count; i++)
            {
                GameObject botGameObject = Object.Instantiate(botPrefab, botsTransforms[i].position, botsTransforms[i].rotation, botsTransforms[i]);
                botGameObject.name = $"Bot {i}";

                var characterController = botGameObject.GetComponent<CharacterController>();
                var colliderBehaviour = botGameObject.GetComponent<ColliderBehaviour>();

                Transform spawnPosition = botsTransforms[i];

                var bot = new Bot(botGameObject, coroutineRunner, spawnPosition, characterController, colliderBehaviour, this);

                _bots.Add(bot);
            }
        }

        public void CreateSpawner(GameObject spawnerPrefab, GameObject resourcePrefab, MonoBehaviour coroutineRunner)
        {
            GameObject spawnerGameObject = Object.Instantiate(spawnerPrefab);
            var spawner = new Spawner(spawnerGameObject, resourcePrefab, this);
            spawner.Spawn(coroutineRunner);
        }

        public void CreateUserInterface(GameObject userInterfacePrefab, GameObject infoPanelPrefab, GameObject goldTextPrefab, GameObject resourceTextPrefab, GameObject sellResourceButton)
        {
            GameObject userInterfaceGameObject = Object.Instantiate(userInterfacePrefab);
            var userInterface = new UserInterface(BotBase, userInterfaceGameObject, infoPanelPrefab, goldTextPrefab, resourceTextPrefab, sellResourceButton);
        }
    }
}