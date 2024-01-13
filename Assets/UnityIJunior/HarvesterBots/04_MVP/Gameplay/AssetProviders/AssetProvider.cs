using UnityEngine;

namespace _03_NoMonobehLogic.Gameplay.AssetProviders
{
    public class AssetProvider
    {
        public AssetProvider(
            GameObject spawnerPrefab,
            GameObject scannerPrefab,
            GameObject basePrefab,
            GameObject botPrefab,
            GameObject resourcePrefab,
            GameObject userInterfacePrefab,
            GameObject infoPanelPrefab,
            GameObject goldTextPrefab,
            GameObject resourceTextPrefab,
            GameObject sellResourceButton)
        {
            SpawnerPrefab = spawnerPrefab;
            ScannerPrefab = scannerPrefab;
            BasePrefab = basePrefab;
            BotPrefab = botPrefab;
            ResourcePrefab = resourcePrefab;
            UserInterfacePrefab = userInterfacePrefab;
            InfoPanelPrefab = infoPanelPrefab;
            GoldTextPrefab = goldTextPrefab;
            ResourceTextPrefab = resourceTextPrefab;
            SellResourceButton = sellResourceButton;
        }

        public GameObject SpawnerPrefab { get; }
        public GameObject ScannerPrefab { get; }
        public GameObject BasePrefab { get; }
        public GameObject BotPrefab { get; }
        public GameObject ResourcePrefab { get; }
        public GameObject UserInterfacePrefab { get; }
        public GameObject InfoPanelPrefab { get; }
        public GameObject GoldTextPrefab { get; }
        public GameObject ResourceTextPrefab { get; }
        public GameObject SellResourceButton { get; }
    }
}