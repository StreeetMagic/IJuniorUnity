using _03_NoMonobehLogic.Gameplay.Bases;
using TMPro;

namespace _03_NoMonobehLogic.UserInterfaces.InfoPanels.GoldTexts
{
    public class GoldText
    {
        private readonly Base _botBase;
        private readonly TextMeshProUGUI _text;

        public GoldText(Base botBase, TextMeshProUGUI text)
        {
            _botBase = botBase;
            _text = text;

            OnGoldCountChanged(_botBase.Gold);
            _botBase.GoldCountChanged += OnGoldCountChanged;
        }

        private void OnGoldCountChanged(int gold)
        {
            _text.text = "Gold: " + gold;
        }
    }
}