using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIText : MonoBehaviour
    {
        public TextMeshPro Text;

        public void SetText(string newText)
        {
            Text.text = newText;
        }
    }
}