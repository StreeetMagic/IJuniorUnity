using System;
using System.Collections;
using System.Collections.Generic;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;

namespace _01_Default.Gameplay.Bots
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private CharacterController _controller;

        private Vector3 _spawnPosition;
        private Resource _target;
        private bool _harvested;
        private Coroutine _movingCoroutine;

        public bool IsBusy { get; private set; }

        private void Start()
        {
            _spawnPosition = transform.position;
        }

        public void SetTarget(Resource target)
        {
            _target = target;

            IsBusy = true;

            Move(_target.transform);
        }

        private void Move(Transform position)
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);

            _movingCoroutine = StartCoroutine(Moving(position));
        }

        private IEnumerator Moving(Transform position)
        {
            float tolerance = 0.1f;

            while (IsAway(position.position, tolerance))
            {
                _controller.Move((position.position - transform.position).normalized * (Time.deltaTime * _moveSpeed));

                yield return null;
            }
        }

        private bool IsAway(Vector3 position, float tolerance) =>
            Math.Abs(transform.position.x - position.x) > tolerance || Math.Abs(transform.position.z - position.z) > tolerance;
    }
}