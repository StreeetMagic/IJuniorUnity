using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Resourcess;
using UnityEngine;

namespace Gameplay.Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointContainer;
        [SerializeField] private Resource _resourcePrefab;

        private List<Vector3> _spawnPoints = new();
        private readonly WaitForSeconds _waitForSeconds = new(0.5f);
        private readonly bool _isSpawning = true;

        private void Awake()
        {
            SetPositions();
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (_isSpawning)
            {
                yield return _waitForSeconds;

                Resource resource = Instantiate(_resourcePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)], Quaternion.identity);
                resource.transform.parent = transform;
            }
        }

        private void SetPositions() =>
            _spawnPoints = _spawnPointContainer.Cast<Transform>()
                .Select(spawnPoint => spawnPoint.position)
                .ToList();
    }
}