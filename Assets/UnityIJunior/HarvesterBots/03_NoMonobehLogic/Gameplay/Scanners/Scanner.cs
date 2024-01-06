using System;
using System.Collections;
using System.Collections.Generic;
using _03_NoMonobehLogic.Gameplay.Resourcess;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Scanners
{
    public class Scanner
    {
        private readonly GameObject _gameObject;
        private readonly bool _isScanning = true;
        private readonly WaitForSeconds _waitForSeconds = new(1f);
        private readonly float _radius = 100f;

        public event Action<List<Resource>> Scanned;

        public Scanner(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void Launch(MonoBehaviour coroutineRunner)
        {
            coroutineRunner.StartCoroutine(Scanning());
        }

        private IEnumerator Scanning()
        {
            while (_isScanning)
            {
                List<Resource> resources = new();

                yield return _waitForSeconds;
                
                foreach (Collider collider1 in Physics.OverlapSphere(_gameObject.transform.position, _radius))
                    if (collider1.TryGetComponent(out ResourceView view))
                        resources.Add(view.Resource);

                Scanned?.Invoke(resources);
            }
        }
    }
}