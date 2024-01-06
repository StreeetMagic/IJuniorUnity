using System;
using System.Collections;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Bots
{
    internal class BotMover
    {
        private readonly GameObject _gameObject;
        private readonly float _moveSpeed = 10f;
        private readonly CharacterController _controller;
        private readonly Transform _spawnPosition;
        private readonly MonoBehaviour _coroutineRunner;

        private Coroutine _movingCoroutine;

        public BotMover(MonoBehaviour coroutineRunner, GameObject gameObject, Transform spawnPosition, CharacterController controller)
        {
            _coroutineRunner = coroutineRunner;
            _gameObject = gameObject;
            _spawnPosition = spawnPosition;
            _controller = controller;
            _gameObject.transform.parent = null;
        }

        public void MoveToSpawnPosition() =>
            Move(_spawnPosition);

        public void Move(Transform position)
        {
            if (_movingCoroutine != null)
                _coroutineRunner.StopCoroutine(_movingCoroutine);

            _movingCoroutine = _coroutineRunner.StartCoroutine(Moving(position));
            
            Debug.Log("Я поехал");
        }

        private IEnumerator Moving(Transform targetTransform)
        {
            float tolerance = 0.1f;

            while (IsAway(targetTransform.position, tolerance, _gameObject.transform.position))
            {
                Vector3 position = targetTransform.position;

                Vector3 targetTransformPosition = new(position.x, 0, position.z);

                _controller.Move((targetTransformPosition - _gameObject.transform.position).normalized * (Time.deltaTime * _moveSpeed));

                Transform transform1 = _controller.transform;
                Vector3 position1 = transform1.position;

                position1 = new Vector3(position1.x, 0, position1.z);
                transform1.position = position1;

                RotateToPosition(targetTransformPosition);

                yield return null;
            }

            _coroutineRunner.StopCoroutine(_movingCoroutine);

            _movingCoroutine = null;
        }

        private void RotateToPosition(Vector3 targetTransformPosition) =>
            _gameObject.transform.LookAt(new Vector3(targetTransformPosition.x, 0, targetTransformPosition.z));

        private bool IsAway(Vector3 position, float tolerance, Vector3 transformPosition) =>
            Math.Abs(transformPosition.x - position.x) > tolerance || Math.Abs(transformPosition.z - position.z) > tolerance;
    }
}