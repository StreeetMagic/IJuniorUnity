using UnityEngine;

namespace _02_DefaultWithFactory.Gameplay.Supplies
{
    public class Supply : MonoBehaviour
    {
        public bool IsHarvested { get; private set; }
        public bool IsMarked { get; private set; }

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