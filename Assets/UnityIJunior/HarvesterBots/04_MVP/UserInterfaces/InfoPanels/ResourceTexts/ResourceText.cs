using _03_NoMonobehLogic.Gameplay.Bases;
using TMPro;

namespace _03_NoMonobehLogic.UserInterfaces.InfoPanels.ResourceTexts
{
    public class ResourceText
    {
        private readonly Base _botBase;
        private readonly TextMeshProUGUI _text;

        public ResourceText(Base botBase, TextMeshProUGUI text)
        {
            _botBase = botBase;
            _text = text;

            OnResourceCountChanged(_botBase.ResourcesCount);
            _botBase.ResourceCountChanged += OnResourceCountChanged;
        }

        private void OnResourceCountChanged(int resources)
        {
            _text.text = "Resources: " + resources;
        }
    }
}