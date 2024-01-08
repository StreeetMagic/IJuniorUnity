using System;
using System.Collections.Generic;
using System.Linq;
using _01_Default.Gameplay.Bots;
using _01_Default.Gameplay.Scanners;
using _01_Default.Gameplay.Supplies;
using UnityEngine;

namespace _01_Default.Gameplay.Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Scanner _scanner;
        [SerializeField] private List<Bot> _bots;

        private readonly int _resourceCost = 10;
        private readonly List<Supply> _targetSupplies = new();
        private readonly List<Supply> _collectedSupplies = new();

        public event Action<int> ResourceCountChanged;
        public event Action<int> GoldCountChanged;

        public int ResourcesCount => _collectedSupplies.Count;
        public int Gold { get; private set; }

        private void OnEnable() =>
            _scanner.Scanned += OnScanned;

        private void OnDisable() =>
            _scanner.Scanned -= OnScanned;

        private void Update() =>
            SetTargets(_bots
                .Where(bot => bot.IsBusy == false)
                .Where(_ => _targetSupplies.Count > 0));

        public void AddResource(Supply supply)
        {
            _collectedSupplies.Add(supply);
            ResourceCountChanged?.Invoke(_collectedSupplies.Count);
            Transform resourceTransform = supply.transform;
            Transform myTransform = transform;
            resourceTransform.parent = myTransform;
            Vector3 position = myTransform.position;
            float offset = 8;
            resourceTransform.position = new Vector3(position.x, offset, position.z);
            supply.GetComponent<Rigidbody>().useGravity = true;
        }

        public void SellResources()
        {
            if (_collectedSupplies.Count <= 0)
                return;

            Gold += _resourceCost * _collectedSupplies.Count;
            GoldCountChanged?.Invoke(Gold);

            foreach (Supply resource in _collectedSupplies)
            {
                Destroy(resource.gameObject);
            }

            _collectedSupplies.Clear();

            ResourceCountChanged?.Invoke(_collectedSupplies.Count);
        }

        private void SetTargets(IEnumerable<Bot> enumerable)
        {
            foreach (Bot bot in enumerable)
            {
                bot.SetTarget(_targetSupplies[0]);
                _targetSupplies[0].Mark();
                _targetSupplies.RemoveAt(0);
            }
        }

        private void OnScanned(List<Supply> resources) =>
            AddResources(resources);

        private void AddResources(List<Supply> resources) =>
            _targetSupplies.AddRange(resources
                .Where(resource => _targetSupplies.Contains(resource) == false
                                   && resource.IsHarvested == false
                                   && resource.IsMarked == false));
    }
}