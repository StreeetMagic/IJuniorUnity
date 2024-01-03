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

        private List<Resource> _targets = new();
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
            List<Bot> freeBots = _bots.Where(bot => bot.IsBusy == false).ToList();

            if (freeBots.Count == 0)
                return;

            Bot freeBot = freeBots[0];

            if (_targets.Count <= 0)
                return;

            Resource resource = _targets[0];

            if (resource.IsMarked)
                return;

            freeBot.SetTarget(resource);
            resource.Mark();
            _targets.Remove(resource);
        }

        private void OnScanned(List<Resource> resources)
        {
            AddResources(resources);
        }

        private void AddResources(List<Resource> resources)
        {
            foreach (Resource resource in resources
                         .Where(resource => _targets.Contains(resource) == false)
                         .Where(resource => resource.IsHarvested == false)
                         .Where(resource => resource.IsMarked == false))
                _targets.Add(resource);
        }

        public void AddResource(Resource resource)
        {
            _resources.Add(resource);

            resource.transform.parent = transform;
            resource.transform.position = new Vector3(transform.position.x, 8, transform.position.z);
            resource.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}