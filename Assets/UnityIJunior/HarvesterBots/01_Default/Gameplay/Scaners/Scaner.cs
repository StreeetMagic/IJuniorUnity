using System;
using System.Collections;
using System.Collections.Generic;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;

namespace _01_Default.Gameplay.Scaners
{
    public class Scaner : MonoBehaviour
    {
        private bool _isScanning = true;
        private WaitForSeconds _waitForSeconds = new(1f);

        public event Action<List<Resource>> Scanned;

        private void Start()
        {
            Scan();
        }

        private void Scan()
        {
            StartCoroutine(Scanning());
        }

        private IEnumerator Scanning()
        {
            while (_isScanning)
            {
                yield return _waitForSeconds;

                var resources = new List<Resource>();

                Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);

                foreach (Collider collider1 in colliders)
                    if (collider1.TryGetComponent(out Resource resource))
                        resources.Add(resource);

                Scanned?.Invoke(resources);
            }
        }
    }
}