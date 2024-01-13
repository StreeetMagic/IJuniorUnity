using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _03_NoMonobehLogic.Gameplay.Factories;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Spawners
{
    public class Spawner
    {
        private readonly GameObject _resourcePrefab;
        private readonly WaitForSeconds _waitForSeconds = new(0.5f);
        private readonly bool _isSpawning = true;
        private readonly GameObject _gameObject;
        private readonly Factory _factory;

        private List<Vector3> _spawnPoints = new();

        public Spawner(GameObject gameObject, GameObject resourcePrefab, Factory factory)
        {
            _gameObject = gameObject;
            _resourcePrefab = resourcePrefab;
            _factory = factory;
            SetPositions();
        }

        public void Spawn(MonoBehaviour coroutineRunner)
        {
            coroutineRunner.StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (_isSpawning)
            {
                yield return _waitForSeconds;
                
                _factory.CreateResource(_resourcePrefab, _spawnPoints, _gameObject);
            }
        }

        private void SetPositions() =>
            _spawnPoints = _gameObject.transform.Cast<Transform>()
                .Select(spawnPoint => spawnPoint.position)
                .ToList();
    }
}