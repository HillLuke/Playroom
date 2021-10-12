using TMPro;

namespace Assets.Scripts.UI
{
    public class InteractText : UIBase
    {
        public TextMeshProUGUI Text;

        protected override void Setup()
        {
            _uIManager.ActionInteractor -= SetInteractText;
            _uIManager.ActionInteractor += SetInteractText;
            base.Setup();
        }

        private void SetInteractText(string text)
        {
            Text.SetText(text);
        }
    }
}