using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapErrorComment : MonoBehaviour
{
    public RectTransform rectTransform;
    public Text title;
    public Text sub;

    public void showErrorTitleComment(string errorComment)
    {
        rectTransform.anchoredPosition = new Vector2(0, 0);
        title.text = errorComment;
    }
    public void showErrorSubComment(string errorComment)
    {
        sub.text = errorComment;
    }

    public void hideErrorComment()
    {
        rectTransform.anchoredPosition = new Vector2(0, 4000);
    }
}
