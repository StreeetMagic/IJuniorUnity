﻿using System;
using System.Collections.Generic;
using System.Linq;
using _03_NoMonobehLogic.Gameplay.Bots;
using _03_NoMonobehLogic.Gameplay.Resourcess;
using _03_NoMonobehLogic.Gameplay.Scanners;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _03_NoMonobehLogic.Gameplay.Bases
{
    public class Base
    {
        private readonly GameObject _gameObject;
        private readonly Scanner _scanner;
        private readonly List<Bot> _bots;
        private readonly int _resourceCost = 10;
        private readonly List<Resource> _targets = new();
        private readonly List<Resource> _resources = new();

        public event Action<int> ResourceCountChanged;
        public event Action<int> GoldCountChanged;

        public int ResourcesCount => _resources.Count;
        public int Gold { get; private set; }

        public Base(Scanner scanner, List<Bot> bots, GameObject gameObject)
        {
            _scanner = scanner;
            _bots = bots;
            _gameObject = gameObject;
            _scanner.Scanned += OnScanned;
            Debug.Log("подписался");
        }

        public void Update()
        {
            SetTargets(_bots
                .Where(bot => bot.IsBusy == false)
                .Where(_ => _targets.Count > 0)
                .ToList());
        }

        private void SetTargets(List<Bot> bots)
        {
            foreach (Bot bot in bots)
            {
                Debug.Log("Ставлю цель боту");
                bot.SetTarget(_targets[0]);
                _targets[0].Mark();
                _targets.RemoveAt(0);

                if (_targets.Count == 0)
                    break;
            }
        }

        private void OnScanned(List<Resource> scannedResources)
        {
            Debug.Log("Обрабатываю скан");
            AddResourcesToHarvest(scannedResources);
        }

        private void AddResourcesToHarvest(List<Resource> scannedResources)
        {
            _targets.AddRange(scannedResources
                .Where(resource => _targets.Contains(resource) == false
                                   && resource.IsHarvested == false
                                   && resource.IsMarked == false));
        }

        public void AddResource(Resource resource)
        {
            _resources.Add(resource);
            ResourceCountChanged?.Invoke(_resources.Count);
            Transform resourceTransform = resource.Transform;
            Transform myTransform = _gameObject.transform;
            resourceTransform.parent = myTransform;
            Vector3 position = myTransform.position;
            resourceTransform.position = new Vector3(position.x, 8, position.z);
            resource.Rigidbody.useGravity = true;
        }

        public void SellResources()
        {
            if (_resources.Count > 0)
            {
                Gold += _resourceCost * _resources.Count;
                GoldCountChanged?.Invoke(Gold);

                foreach (Resource resource in _resources)
                {
                    Object.Destroy(resource.GameObject);
                }

                _resources.Clear();

                ResourceCountChanged?.Invoke(_resources.Count);
            }
        }
    }
}