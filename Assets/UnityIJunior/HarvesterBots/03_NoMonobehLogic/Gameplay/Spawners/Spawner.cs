using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _03_NoMonobehLogic.Gameplay.Resourcess;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Spawners
{
    public class Spawner
    {
        private GameObject _resourcePrefab;

        private List<Vector3> _spawnPoints = new();
        private readonly WaitForSeconds _waitForSeconds = new(0.5f);
        private readonly bool _isSpawning = true;
        private readonly GameObject _gameObject;

        public Spawner(GameObject gameObject, GameObject resourcePrefab)
        {
            _gameObject = gameObject;
            _resourcePrefab = resourcePrefab;
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

                GameObject resource = Object.Instantiate(_resourcePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)], Quaternion.identity);
                resource.transform.parent = _gameObject.transform;
            }
        }

        private void SetPositions() =>
            _spawnPoints = _gameObject.transform.Cast<Transform>()
                .Select(spawnPoint => spawnPoint.position)
                .ToList();
    }
}