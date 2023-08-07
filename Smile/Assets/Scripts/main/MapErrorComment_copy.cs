using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapErrorComment_copy : MonoBehaviour
{
    public Image UI;
    public Text title;

    public void showErrorTitleComment(string errorComment)
    {
        StopAllCoroutines();
        SetAlpha(1.0f);
        title.text = errorComment;
        StartCoroutine(FadeImage(1.0f, 0.0f, 1.0f));
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        yield return new WaitForSeconds(4.0f);

        float currentTime = 0.0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentTime / duration);
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            SetAlpha(currentAlpha);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = UI.color;
        color.a = alpha;
        UI.color = color;
        title.color = color;
    }
}
