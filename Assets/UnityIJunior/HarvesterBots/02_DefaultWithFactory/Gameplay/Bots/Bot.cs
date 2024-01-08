using _02_DefaultWithFactory.Gameplay.Bases;
using _02_DefaultWithFactory.Gameplay.Supplies;
using UnityEngine;

namespace _02_DefaultWithFactory.Gameplay.Bots
{
    [RequireComponent(typeof(BotMover))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private BotMover _botMover;
        private Supply _target;

        public bool IsBusy { get; private set; }

        private void Awake()
        {
            _botMover = GetComponent<BotMover>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            DetectResource(otherCollider);
            DetectBase(otherCollider);
        }

        public void SetTarget(Supply target)
        {
            _target = target;
            IsBusy = true;
            _botMover.Move(_target.transform);
        }

        private void DetectResource(Collider other)
        {
            if (other.TryGetComponent(out Supply resource) == false)
                return;

            if (resource != _target)
                return;

            _target.Harvest();
            Transform myTransform = transform;
            Transform targetTransform = _target.transform;
            targetTransform.parent = myTransform;
            Vector3 transformPosition = myTransform.position;
            float offset = 2;
            targetTransform.position = new Vector3(transformPosition.x, offset, transformPosition.z);
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