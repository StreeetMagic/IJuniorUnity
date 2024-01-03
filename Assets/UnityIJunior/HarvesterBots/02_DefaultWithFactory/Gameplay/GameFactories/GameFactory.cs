using System.Collections.Generic;
using _02_DefaultWithFactory.Gameplay.Bases;
using _02_DefaultWithFactory.Gameplay.Bots;
using _02_DefaultWithFactory.Gameplay.Scanners;
using _02_DefaultWithFactory.Gameplay.Spawners;
using _02_DefaultWithFactory.UserInterfaces;
using UnityEngine;

namespace _02_DefaultWithFactory.Gameplay.GameFactories
{
    public class GameFactory : MonoBehaviour
    {
        [SerializeField] private Spawner _spawnerPrefab;
        [SerializeField] private Scanner _scannerPrefab;
        [SerializeField] private Base _basePrefab;
        [SerializeField] private Bot _botPrefab;
        [SerializeField] private UserInterface _userInterfacePrefab;
        [SerializeField] private Transform _basePosition;
        [SerializeField] private List<Transform> _botsTransforms;

        private void Awake()
        {
            List<Bot> bots = new();

            foreach (Transform botTransform in _botsTransforms)
            {
                Bot bot = Instantiate(_botPrefab, botTransform.position, botTransform.rotation).GetComponent<Bot>();

                bots.Add(bot);
            }

            Instantiate(_spawnerPrefab);
            Scanner scanner = Instantiate(_scannerPrefab);

            var botBase = Instantiate(_basePrefab, _basePosition);
            var botBaseComponent = botBase.GetComponent<Base>();
            botBaseComponent.Init(scanner, bots);

            Instantiate(_userInterfacePrefab, botBase.transform).Init(botBase);
        }
    }
}