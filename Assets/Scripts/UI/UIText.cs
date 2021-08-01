using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    public TextMeshPro Text;

    public void SetText(string newText)
    {
        Text.text = newText;
    }
}