using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Supplies
{
    public class Supply
    {
        public Supply(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public GameObject GameObject { get; }
        public bool IsHarvested { get; private set; }
        public bool IsMarked { get; private set; }
        public Transform Transform => GameObject.transform;
        public Rigidbody Rigidbody => GameObject.GetComponent<Rigidbody>();

        public void Harvest()
        {
            IsHarvested = true;
        }

        public void Mark()
        {
            IsMarked = true;
        }
    }
}