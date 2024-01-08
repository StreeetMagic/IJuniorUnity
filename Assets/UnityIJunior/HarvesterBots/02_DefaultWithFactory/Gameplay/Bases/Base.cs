using System;
using System.Collections.Generic;
using System.Linq;
using _02_DefaultWithFactory.Gameplay.Bots;
using _02_DefaultWithFactory.Gameplay.Scanners;
using _02_DefaultWithFactory.Gameplay.Supplies;
using UnityEngine;

namespace _02_DefaultWithFactory.Gameplay.Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Scanner _scanner;
        [SerializeField] private List<Bot> _bots;

        private readonly int _resourceCost = 10;
        private readonly List<Supply> _targets = new();
        private readonly List<Supply> _resources = new();

        public event Action<int> ResourceCountChanged;
        public event Action<int> GoldCountChanged;

        public int ResourcesCount => _resources.Count;
        public int Gold { get; private set; }

        private void OnDestroy()
        {
            _scanner.Scanned -= OnScanned;
        }

        private void Update() =>
            SetTargets(_bots
                .Where(bot => bot.IsBusy == false)
                .Where(_ => _targets.Count > 0));

        public void AddResource(Supply supply)
        {
            _resources.Add(supply);
            ResourceCountChanged?.Invoke(_resources.Count);
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
            if (_resources.Count <= 0)
                return;

            Gold += _resourceCost * _resources.Count;
            GoldCountChanged?.Invoke(Gold);

            foreach (Supply resource in _resources)
            {
                Destroy(resource.gameObject);
            }

            _resources.Clear();

            ResourceCountChanged?.Invoke(_resources.Count);
        }

        public void Init(Scanner scanner, List<Bot> bots)
        {
            _scanner = scanner;
            _bots = bots;
            _scanner.Scanned += OnScanned;
        }

        private void SetTargets(IEnumerable<Bot> enumerable)
        {
            foreach (Bot bot in enumerable)
            {
                bot.SetTarget(_targets[0]);
                _targets[0].Mark();
                _targets.RemoveAt(0);
            }
        }

        private void OnScanned(List<Supply> resources) =>
            AddResources(resources);

        private void AddResources(List<Supply> resources) =>
            _targets.AddRange(resources
                .Where(resource => _targets.Contains(resource) == false
                                   && resource.IsHarvested == false
                                   && resource.IsMarked == false));
    }
}