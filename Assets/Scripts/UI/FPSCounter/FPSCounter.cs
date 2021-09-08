using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.FPSCounter
{
    public class FPSCounter : MonoBehaviour
    {
        public int AverageFrameRate;
        public TextMeshProUGUI Text;

        private void Start()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            AverageFrameRate = (int)current;
            Text.text = AverageFrameRate.ToString() + " FPS";
        }
    }
}