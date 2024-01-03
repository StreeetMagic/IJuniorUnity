using _03_NoMonobehLogic.Gameplay.Bases;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _03_NoMonobehLogic.UserInterfaces
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resourceCount;
        [SerializeField] private TextMeshProUGUI _goldCount;
        [SerializeField] private Button _sellButton;

        private Base _base;

        public void Init(Base base1)
        {
            _base = base1;
            OnResourceCountChanged(_base.ResourcesCount);
            OnGoldCountChanged(_base.Gold);

            _base.ResourceCountChanged += OnResourceCountChanged;
            _base.GoldCountChanged += OnGoldCountChanged;
            _sellButton.onClick.AddListener(OnSellButton);
        }

        private void OnDestroy()
        {
            _base.ResourceCountChanged -= OnResourceCountChanged;
            _base.GoldCountChanged -= OnGoldCountChanged;
            _sellButton.onClick.RemoveListener(OnSellButton);
        }

        private void OnResourceCountChanged(int resources)
        {
            _resourceCount.text = "Resources: " + resources;
        }

        private void OnGoldCountChanged(int gold)
        {
            _goldCount.text = "Gold: " + gold;
        }

        private void OnSellButton()
        {
            _base.SellResources();
        }
    }
}