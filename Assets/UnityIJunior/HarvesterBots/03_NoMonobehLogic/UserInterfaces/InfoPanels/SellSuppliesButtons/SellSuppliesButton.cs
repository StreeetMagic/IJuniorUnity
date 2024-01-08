using _03_NoMonobehLogic.Gameplay.Bases;
using UnityEngine.UI;

namespace _03_NoMonobehLogic.UserInterfaces.InfoPanels.SellSuppliesButtons
{
    public class SellSuppliesButton
    {
        private readonly Base _botBase;

        public SellSuppliesButton(Button button, Base botBase)
        {
            _botBase = botBase;
            button.onClick.AddListener(OnSellButton);
        }

        private void OnSellButton()
        {
            _botBase.SellResources();
        }
    }
}