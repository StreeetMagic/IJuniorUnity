using System;
using System.Collections;
using System.Collections.Generic;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Default.Gameplay.Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointContainer;
        [SerializeField] private Resource _resourcePrefab;

        private List<Vector3> _spawnPoints;
        private WaitForSeconds _waitForSeconds = new(0.5f);
        private bool _isSpawning = true;

        private void Awake()
        {
            SetPositions();
            Spawn();
        }

        private void Spawn()
        {
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (_isSpawning)
            {
                yield return _waitForSeconds;

                Vector3 randomPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

                Instantiate(_resourcePrefab, randomPosition, Quaternion.identity);
            }
        }

        private void SetPositions()
        {
            _spawnPoints = new List<Vector3>();

            for (int i = 0; i < _spawnPointContainer.childCount; i++)
                _spawnPoints.Add(_spawnPointContainer.GetChild(i).position);
        }
    }
}