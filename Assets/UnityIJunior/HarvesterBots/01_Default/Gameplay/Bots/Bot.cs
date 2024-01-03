using System;
using System.Collections;
using _01_Default.Gameplay.Bases;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01_Default.Gameplay.Bots
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Transform _spawnPosition;

        private Resource _target;
        private Coroutine _movingCoroutine;

        public bool IsBusy { get; private set; }

        private void Start()
        {
            _spawnPosition.transform.parent = null;
        }

        public void SetTarget(Resource target)
        {
            _target = target;

            IsBusy = true;

            Move(_target.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Resource resource))
            {
                if (resource == _target)
                {
                    _target.Harvest();
                    Transform myTransform = transform;
                    Transform targetTransform = _target.transform;
                    targetTransform.parent = myTransform;
                    Vector3 transformPosition = myTransform.position;
                    targetTransform.position = new Vector3(transformPosition.x, 2, transformPosition.z);
                    _target.GetComponent<Rigidbody>().useGravity = false;
                    Move(_spawnPosition);
                }
            }

            if (other.TryGetComponent(out Base botBase))
            {
                if (_target != null)
                {
                    botBase.AddResource(_target);
                    _target = null;
                    IsBusy = false;
                }
            }
        }

        private void Move(Transform position)
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);

            _movingCoroutine = StartCoroutine(Moving(position));
        }

        private IEnumerator Moving(Transform targetTransform)
        {
            float tolerance = 0.1f;

            while (IsAway(targetTransform.position, tolerance))
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

        private bool IsAway(Vector3 position, float tolerance) =>
            Math.Abs(transform.position.x - position.x) > tolerance
            || Math.Abs(transform.position.z - position.z) > tolerance;
    }
}