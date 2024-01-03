﻿using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.Resourcess
{
    public class Resource : MonoBehaviour
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