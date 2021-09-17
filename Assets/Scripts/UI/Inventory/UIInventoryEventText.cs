using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.InventorySystem
{
    public class UIInventoryEventText : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public float DestroyAfter = 2f;
        public float FadeTime = 0.4f;
        public bool Fade = false;

        public void SetText(string text)
        {
            Text.text = text;
            StartCoroutine(FadeTextToFullAlpha(DestroyAfter));
        }

        public IEnumerator FadeTextToFullAlpha(float time)
        {
            yield return new WaitForSeconds(time);
            Text.CrossFadeAlpha(0.0f, 0.5f, true);
            yield return new WaitForSeconds(FadeTime);
            Destroy(gameObject);
        }
    }
}