using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public GameObject content;
    public ScrollRect scrollRect;

    // Call this method whenever new text is added
    public void ScrollToBottom()
    {
        // Get the Scroll Rect component form teh content GameObject
        RectTransform contentRectTransform = content.GetComponentInChildren<RectTransform > ();

        // Calculate the maximum Y position
        float maxY = Mathf.Max(0, contentRectTransform.sizeDelta.y - scrollRect.viewport.rect.height);

        // Set the Y position to the maximum value
        contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, maxY);
    }
}

