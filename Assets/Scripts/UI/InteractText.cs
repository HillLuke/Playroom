using TMPro;

namespace Assets.Scripts.UI
{
    public class InteractText : UIBase
    {
        public TextMeshProUGUI Text;

        protected override void Setup()
        {
            //_UIManager.ActionInteractor -= SetInteractText;
            _UIManager.ActionInteractor += SetInteractText;
            base.Setup();
        }

        private void SetInteractText(string text)
        {
            Text.SetText(text);
        }
    }
}