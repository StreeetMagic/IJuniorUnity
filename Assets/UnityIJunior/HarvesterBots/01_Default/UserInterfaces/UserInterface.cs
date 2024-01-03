using _01_Default.Gameplay.Bases;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _01_Default.UserInterfaces
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private Base _base;

        [SerializeField] private TextMeshProUGUI _resourceCount;
        [SerializeField] private TextMeshProUGUI _goldCount;
        [SerializeField] private Button _sellButton;

        private void OnEnable()
        {
            OnResourceCountChanged(_base.ResourcesCount);
            OnGoldCountChanged(_base.Gold);

            _base.ResourceCountChanged += OnResourceCountChanged;
            _base.GoldCountChanged += OnGoldCountChanged;
            _sellButton.onClick.AddListener(OnSellButton);
        }

        private void OnDisable()
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