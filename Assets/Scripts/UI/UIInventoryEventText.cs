using System.Collections;
using TMPro;
using UnityEngine;

public class UIInventoryEventText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float DestroyAfter = 2f;
    public bool Fade = false;

    public void SetText(string text)
    {
        Text.text = text;
        StartCoroutine(FadeTextToFullAlpha(DestroyAfter));
    }

    private void Update()
    {
        if (Fade)
        {
            Text.CrossFadeAlpha(0.0f, 2f, false);
        }
    }

    public IEnumerator FadeTextToFullAlpha(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}