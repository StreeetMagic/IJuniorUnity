using _01_Default.Gameplay.Bases;
using _01_Default.Gameplay.Supplies;
using UnityEngine;

namespace _01_Default.Gameplay.Bots
{
    [RequireComponent(typeof(BotMover))]
    public class Bot : MonoBehaviour
    {
        private BotMover _botMover;
        private Supply _targetSupply;
        
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
            _targetSupply = target;
            IsBusy = true;
            _botMover.Move(_targetSupply.transform);
        }

        private void DetectResource(Collider other)
        {
            if (other.TryGetComponent(out Supply resource) == false)
                return;

            if (resource != _targetSupply)
                return;

            _targetSupply.Harvest();
            Transform myTransform = transform;
            Transform targetTransform = _targetSupply.transform;
            targetTransform.parent = myTransform;
            Vector3 transformPosition = myTransform.position;
            float offset = 2;
            targetTransform.position = new Vector3(transformPosition.x, offset, transformPosition.z);
            _targetSupply.GetComponent<Rigidbody>().useGravity = false;
            _botMover.MoveToSpawnPosition();
        }

        private void DetectBase(Collider other)
        {
            if (other.TryGetComponent(out Base botBase) == false)
                return;

            if (_targetSupply == null)
                return;

            botBase.AddResource(_targetSupply);
            _targetSupply = null;
            IsBusy = false;
        }
    }
}