using System;
using System.Collections;
using System.Collections.Generic;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;

namespace _01_Default.Gameplay.Scanners
{
    public class Scanner : MonoBehaviour
    {
        private readonly bool _isScanning = true;
        private readonly WaitForSeconds _waitForSeconds = new(1f);
        private readonly float _radius = 100f;

        public event Action<List<Resource>> Scanned;

        private void Start() =>
            StartCoroutine(Scanning(new List<Resource>()));

        private IEnumerator Scanning(List<Resource> resources)
        {
            while (_isScanning)
            {
                yield return _waitForSeconds;

                foreach (Collider collider1 in Physics.OverlapSphere(transform.position, _radius))
                    if (collider1.TryGetComponent(out Resource resource))
                        resources.Add(resource);

                Scanned?.Invoke(resources);
            }
        }
    }
}