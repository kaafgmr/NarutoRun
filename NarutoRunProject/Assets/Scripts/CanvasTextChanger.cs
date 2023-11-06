using UnityEngine;
using TMPro;

public class CanvasTextChanger : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    public static CanvasTextChanger instance;

    private void Start()
    {
        instance = this;
    }

    public void UpdateText(string text)
    {
        UIText.text = text;
    }

    public void Showtext()
    {
        UIText.enabled = true;
        UpdateText(UIText.text);
    }

    public void hideText()
    {
        UIText.enabled = false;
    }
}
