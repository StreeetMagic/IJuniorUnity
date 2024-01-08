using _03_NoMonobehLogic.Gameplay.Bases;
using _03_NoMonobehLogic.UserInterfaces.InfoPanels.GoldTexts;
using _03_NoMonobehLogic.UserInterfaces.InfoPanels.ResourceTexts;
using _03_NoMonobehLogic.UserInterfaces.InfoPanels.SellResourceButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _03_NoMonobehLogic.UserInterfaces
{
    public class UserInterface
    {
        public UserInterface(
            Base botBase,
            GameObject gameObject,
            GameObject infoPanelPrefab,
            GameObject goldTextPrefab,
            GameObject resourceTextPrefab,
            GameObject sellResourceButton)
        {
            Canvas cavnas = CreateCavnas(gameObject, infoPanelPrefab, out Transform infoPanelTransform);
            CreateGoldText(botBase, goldTextPrefab, infoPanelTransform);
            CreateResourceText(botBase, resourceTextPrefab, infoPanelTransform);
            CreateSellResourceButton(botBase, sellResourceButton, cavnas);
        }

        private Canvas CreateCavnas(GameObject gameObject, GameObject infoPanelPrefab, out Transform infoPanelTransform)
        {
            var cavnas = gameObject.GetComponentInChildren<Canvas>();
            GameObject infoPanelGameObject = Object.Instantiate(infoPanelPrefab, cavnas.transform);
            infoPanelTransform = infoPanelGameObject.transform;
            return cavnas;
        }

        private void CreateGoldText(Base botBase, GameObject goldTextPrefab, Transform infoPanelTransform)
        {
            GameObject goldTextGameObject = Object.Instantiate(goldTextPrefab, infoPanelTransform);
            new GoldText(botBase, goldTextGameObject.GetComponent<TextMeshProUGUI>());
        }

        private void CreateResourceText(Base botBase, GameObject resourceTextPrefab, Transform infoPanelTransform)
        {
            GameObject resourceTextGameObject = Object.Instantiate(resourceTextPrefab, infoPanelTransform);
            new ResourceText(botBase, resourceTextGameObject.GetComponent<TextMeshProUGUI>());
        }

        private void CreateSellResourceButton(Base botBase, GameObject sellResourceButton, Canvas cavnas)
        {
            GameObject sellResourceButtonGameObject = Object.Instantiate(sellResourceButton, cavnas.transform);
            new SellResourceButton(sellResourceButtonGameObject.GetComponent<Button>(), botBase);
        }
    }
}