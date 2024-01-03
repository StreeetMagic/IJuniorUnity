using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Bots;
using Gameplay.Resourcess;
using Gameplay.Scanners;
using UnityEngine;

namespace Gameplay.Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Scanner _scanner;
        [SerializeField] private List<Bot> _bots;

        private readonly int _resourceCost = 10;
        private readonly List<Resource> _targets = new();
        private readonly List<Resource> _resources = new();

        public event Action<int> ResourceCountChanged;
        public event Action<int> GoldCountChanged;

        public int ResourcesCount => _resources.Count;
        public int Gold { get; private set; }

        private void OnEnable() =>
            _scanner.Scanned += OnScanned;

        private void OnDisable() =>
            _scanner.Scanned -= OnScanned;

        private void Update() =>
            SetTargets(_bots
                .Where(bot => bot.IsBusy == false)
                .Where(_ => _targets.Count > 0));

        private void SetTargets(IEnumerable<Bot> enumerable)
        {
            foreach (Bot bot in enumerable)
            {
                bot.SetTarget(_targets[0]);
                _targets[0].Mark();
                _targets.RemoveAt(0);
            }
        }

        private void OnScanned(List<Resource> resources) =>
            AddResources(resources);

        private void AddResources(List<Resource> resources) =>
            _targets.AddRange(resources
                .Where(resource => _targets.Contains(resource) == false
                                   && resource.IsHarvested == false
                                   && resource.IsMarked == false));

        public void AddResource(Resource resource)
        {
            _resources.Add(resource);
            ResourceCountChanged?.Invoke(_resources.Count);
            Transform resourceTransform = resource.transform;
            Transform myTransform = transform;
            resourceTransform.parent = myTransform;
            Vector3 position = myTransform.position;
            resourceTransform.position = new Vector3(position.x, 8, position.z);
            resource.GetComponent<Rigidbody>().useGravity = true;
        }

        public void SellResources()
        {
            if (_resources.Count > 0)
            {
                Gold += _resourceCost * _resources.Count;
                GoldCountChanged?.Invoke(Gold);

                foreach (Resource resource in _resources)
                {
                    Destroy(resource.gameObject);
                }

                _resources.Clear();

                ResourceCountChanged?.Invoke(_resources.Count);
            }
        }
    }
}