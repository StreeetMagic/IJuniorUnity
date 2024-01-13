using _03_NoMonobehLogic.Gameplay.Factories;
using _03_NoMonobehLogic.Gameplay.Supplies;
using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Bots
{
    public class Bot
    {
        private readonly GameObject _gameObject;
        private readonly BotMover _botMover;
        private readonly ColliderBehaviour _botColliderBehaviour;
        private readonly Factory _factory;

        private Supply _target;

        public Bot(GameObject gameObject, MonoBehaviour coroutineRunner, Transform spawnPosition, CharacterController controller, ColliderBehaviour botColliderBehaviour, Factory factory)
        {
            _gameObject = gameObject;
            _botColliderBehaviour = botColliderBehaviour;
            _factory = factory;
            _botMover = new BotMover(coroutineRunner, gameObject, spawnPosition, controller);
            _botColliderBehaviour.TriggerEnter += OnTriggerEnter;
        }

        public bool IsBusy { get; private set; }

        private void OnTriggerEnter(Collider otherCollider)
        {
            DetectResource(otherCollider);
            DetectBase(otherCollider);
        }

        public void SetTarget(Supply target)
        {
            _target = target;
            IsBusy = true;
            _botMover.Move(_target.Transform);
        }

        private void DetectResource(Collider other)
        {
            if (_factory.Resources.ContainsKey(other.gameObject) == false)
                return;

            if (_factory.Resources[other.gameObject] != _target)
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
            if (_target == null)
                return;
            
            if (_factory.Bases.ContainsKey(other.gameObject) == false)
                return;

            _factory.Bases[other.gameObject].AddResource(_target);
            _target = null;
            IsBusy = false;
        }
    }
}