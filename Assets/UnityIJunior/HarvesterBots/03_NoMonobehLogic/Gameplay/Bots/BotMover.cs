using System;
using System.Collections;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Bots
{
    internal class BotMover
    {
        private float _moveSpeed = 10f;
        private CharacterController _controller;
        private Transform _spawnPosition;

        private Coroutine _movingCoroutine;

        private void Start() =>
            _spawnPosition.transform.parent = null;

        public void MoveToSpawnPosition() =>
            Move(_spawnPosition);

        public void Move(Transform position)
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);

            _movingCoroutine = StartCoroutine(Moving(position));
        }

        private IEnumerator Moving(Transform targetTransform)
        {
            float tolerance = 0.1f;

            while (IsAway(targetTransform.position, tolerance, transform.position))
            {
                Vector3 position = targetTransform.position;

                Vector3 targetTransformPosition = new(position.x, 0, position.z);

                _controller.Move((targetTransformPosition - transform.position).normalized * (Time.deltaTime * _moveSpeed));

                Transform transform1 = _controller.transform;
                Vector3 position1 = transform1.position;

                position1 = new Vector3(position1.x, 0, position1.z);
                transform1.position = position1;

                RotateToPosition(targetTransformPosition);

                yield return null;
            }

            StopCoroutine(_movingCoroutine);

            _movingCoroutine = null;
        }

        private void RotateToPosition(Vector3 targetTransformPosition) =>
            transform.LookAt(new Vector3(targetTransformPosition.x, 0, targetTransformPosition.z));

        private bool IsAway(Vector3 position, float tolerance, Vector3 transformPosition) =>
            Math.Abs(transformPosition.x - position.x) > tolerance || Math.Abs(transformPosition.z - position.z) > tolerance;
    }
}