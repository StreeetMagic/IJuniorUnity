using _03_NoMonobehLogic.Gameplay.Bases;
using UnityEngine.UI;

namespace _03_NoMonobehLogic.UserInterfaces.InfoPanels.SellResourceButtons
{
    public class SellResourceButton
    {
        private readonly Base _botBase;

        public SellResourceButton(Button button, Base botBase)
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