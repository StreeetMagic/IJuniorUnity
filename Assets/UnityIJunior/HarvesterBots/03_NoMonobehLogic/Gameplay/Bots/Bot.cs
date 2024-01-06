using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.Gameplay.Resourcess;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Bots
{
    public class Bot
    {
        private readonly GameObject _gameObject;
        private readonly BotMover _botMover;
        private readonly ColliderBehaviour _botColliderBehaviour;
        private Resource _target;

        public bool IsBusy { get; private set; }

        public Bot(GameObject gameObject, MonoBehaviour coroutineRunner, Transform spawnPosition, CharacterController controller, ColliderBehaviour botColliderBehaviour)
        {
            _gameObject = gameObject;
            _botColliderBehaviour = botColliderBehaviour;
            _botMover = new BotMover(coroutineRunner, gameObject, spawnPosition, controller);
            _botColliderBehaviour.TriggerEnter += OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            DetectResource(otherCollider);
            DetectBase(otherCollider);
        }

        public void SetTarget(Resource target)
        {
            _target = target;
            IsBusy = true;
            _botMover.Move(_target.Transform);
        }

        private void DetectResource(Collider other)
        {
            if (other.TryGetComponent(out ResourceView view) == false)
                return;

            if (view.Resource != _target)
                return;

            _target.Harvest();
            Transform myTransform = _gameObject.transform;
            Transform targetTransform = _target.Transform;
            targetTransform.parent = myTransform;
            Vector3 transformPosition = myTransform.position;
            targetTransform.position = new Vector3(transformPosition.x, 2, transformPosition.z);
            _target.Rigidbody.useGravity = false;
            _botMover.MoveToSpawnPosition();
        }

        private void DetectBase(Collider other)
        {
            if (other.TryGetComponent(out BaseView view) == false)
                return;

            if (_target == null)
                return;

            view.Base.AddResource(_target);
            _target = null;
            IsBusy = false;
        }
    }
}