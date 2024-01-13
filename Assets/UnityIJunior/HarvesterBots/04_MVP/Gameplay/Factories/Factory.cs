using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.AssetProviders;
using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.Gameplay.Bots;
using _03_NoMonobehLogic.Gameplay.Scanners;
using _03_NoMonobehLogic.Gameplay.Spawners;
using _03_NoMonobehLogic.Gameplay.Supplies;
using _03_NoMonobehLogic.UserInterfaces;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Factories
{
    public class Factory
    {
        private readonly Dictionary<GameObject, Supply> _resources = new();
        private readonly Dictionary<GameObject, Base> _bases = new();

        private Scanner _scanner;
        private List<Bot> _bots;

        public IReadOnlyDictionary<GameObject, Supply> Resources => _resources;
        public IReadOnlyDictionary<GameObject, Base> Bases => _bases;
        
        public Base BotBase { get; private set; }

        public void CreateResource(GameObject resourcePrefab, List<Vector3> spawnPoints, GameObject parent)
        {
            GameObject gameObject = Object.Instantiate(resourcePrefab, spawnPoints[Random.Range(0, spawnPoints.Count)], Quaternion.identity);
            gameObject.transform.parent = parent.transform;
            Supply supply = new(gameObject);

            _resources.Add(gameObject, supply);
        }

        public void CreateBase(AssetProvider assetProvider, Transform basePosition)
        {
            GameObject baseGameObject = Object.Instantiate(assetProvider.BasePrefab, basePosition.position, basePosition.rotation);
            BotBase = new Base(_scanner, _bots, baseGameObject);

            _bases.Add(baseGameObject, BotBase);
        }

        public void CreateScanner(AssetProvider assetProvider, MonoBehaviour coroutineRunner)
        {
            GameObject scannerGameObject = Object.Instantiate(assetProvider.ScannerPrefab);
            _scanner = new Scanner(scannerGameObject, this);
            _scanner.Launch(coroutineRunner);
        }

        public void CreateBots(AssetProvider assetProvider, List<Transform> botsTransforms, MonoBehaviour coroutineRunner)
        {
            _bots = new List<Bot>();

            for (int i = 0; i < botsTransforms.Count; i++)
            {
                GameObject botGameObject = Object.Instantiate(assetProvider.BotPrefab, botsTransforms[i].position, botsTransforms[i].rotation, botsTransforms[i]);
                botGameObject.name = $"Bot {i}";

                var characterController = botGameObject.GetComponent<CharacterController>();
                var colliderBehaviour = botGameObject.GetComponent<ColliderBehaviour>();

                Transform spawnPosition = botsTransforms[i];

                var bot = new Bot(botGameObject, coroutineRunner, spawnPosition, characterController, colliderBehaviour, this);

                _bots.Add(bot);
            }
        }

        public void CreateSpawner(AssetProvider assetProvider, MonoBehaviour coroutineRunner)
        {
            GameObject spawnerGameObject = Object.Instantiate(assetProvider.SpawnerPrefab);
            var spawner = new Spawner(spawnerGameObject, assetProvider.ResourcePrefab, this);
            spawner.Spawn(coroutineRunner);
        }

        public void CreateUserInterface(AssetProvider assetProvider)
        {
            GameObject userInterfaceGameObject = Object.Instantiate(assetProvider.UserInterfacePrefab);
            var userInterface = new UserInterface(BotBase, userInterfaceGameObject, assetProvider.InfoPanelPrefab, assetProvider.GoldTextPrefab, assetProvider.ResourceTextPrefab, assetProvider.SellResourceButton);
        }
    }
}