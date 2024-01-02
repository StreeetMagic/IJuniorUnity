using System;
using System.Collections.Generic;
using System.Linq;
using _01_Default.Gameplay.Bots;
using _01_Default.Gameplay.Resourcess;
using _01_Default.Gameplay.Scaners;
using UnityEngine;

namespace _01_Default.Gameplay.Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Scaner _scaner;
        [SerializeField] private List<Bot> _bots;

        private List<Resource> _resources = new();

        private void OnEnable()
        {
            _scaner.Scanned += OnScanned;
        }

        private void OnDisable()
        {
            _scaner.Scanned -= OnScanned;
        }

        private void Update()
        {
            SetTargets();
        }

        private void SetTargets()
        {
            List<Bot> freeBots = _bots
                .Where(bot => bot.IsBusy == false)
                .ToList();

            foreach (Bot bot in freeBots)
            {
                if (_resources.Count == 0)
                    break;

                Resource resource = _resources[0];

                if (resource.IsMarked == false)
                {
                    resource.Mark();
                    bot.SetTarget(resource);
                    _resources.Remove(resource);
                }
            }
        }

        private void OnScanned(List<Resource> resources)
        {
            AddResources(resources);
        }

        private void AddResources(List<Resource> resources)
        {
            foreach (Resource resource in resources
                         .Where(resource => _resources.Contains(resource) == false)
                         .Where(resource => resource.IsHarvested == false))
                _resources.Add(resource);
        }
    }
}