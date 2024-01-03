using _01_Default.Gameplay.Bases;
using _01_Default.Gameplay.Resourcess;
using UnityEngine;

namespace _01_Default.Gameplay.Bots
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private BotMover _botMover;

        private Resource _target;

        public bool IsBusy { get; private set; }

        private void OnTriggerEnter(Collider otherCollider)
        {
            DetectResource(otherCollider);
            DetectBase(otherCollider);
        }

        public void SetTarget(Resource target)
        {
            _target = target;
            IsBusy = true;
            _botMover.Move(_target.transform);
        }

        private void DetectResource(Collider other)
        {
            if (other.TryGetComponent(out Resource resource) == false)
                return;

            if (resource != _target)
                return;

            _target.Harvest();
            Transform myTransform = transform;
            Transform targetTransform = _target.transform;
            targetTransform.parent = myTransform;
            Vector3 transformPosition = myTransform.position;
            targetTransform.position = new Vector3(transformPosition.x, 2, transformPosition.z);
            _target.GetComponent<Rigidbody>().useGravity = false;
            _botMover.MoveToSpawnPosition();
        }

        private void DetectBase(Collider other)
        {
            if (other.TryGetComponent(out Base botBase) == false)
                return;

            if (_target == null)
                return;

            botBase.AddResource(_target);
            _target = null;
            IsBusy = false;
        }
    }
}