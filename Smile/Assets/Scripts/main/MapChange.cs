using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject[] map = new GameObject[6];

    /*
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "EasyBtn":
                UniteData.Difficulty = 1;
                SceneManager.LoadScene("Easy Map");
                break;
            case "NomalBtn":
                UniteData.Difficulty = 2;
                SceneManager.LoadScene("Nomal Map");
                break;
        }
    }
    */

    public void EasyBtn()
    {
        UniteData.Difficulty = 1;
        StartCoroutine(ScaleDownCoroutine(0, "Easy Map"));
    }

    public void EasyBtn2()
    {
        UniteData.Difficulty = 4;
        StartCoroutine(ScaleDownCoroutine(3, "Easy Map2"));
    }

    public void NormalBtn()
    {
        UniteData.Difficulty = 2;
        StartCoroutine(ScaleDownCoroutine(1, "Nomal Map"));
    }

    public void HardBtn()
    {
        UniteData.Difficulty = 3;
        StartCoroutine(ScaleDownCoroutine(2, "Hard Map"));
    }

    private IEnumerator ScaleDownCoroutine(int idx, string mapName)
    {
        float duration = 0.1f;
        float timer = 0f;
        Vector3 initialScale = new Vector3(1.0f, 1.0f, 1.0f);
        GameObject ping = map[idx].transform.GetChild(0).gameObject;
        GameObject title = map[idx].transform.GetChild(1).gameObject;
        Vector3 initialPingPosition = ping.transform.position;
        Vector3 targetPingPosition = initialPingPosition + new Vector3(0, -10, 0);
        Vector3 initialTitlePosition = title.transform.position;
        Vector3 targetTitlePosition = initialTitlePosition + new Vector3(0, 10, 0);

        while (timer < duration)
        {
            float scaleFactor = Mathf.Lerp(1f, 0.9f, timer / duration);
            float moveFactor = Mathf.Lerp(0f, 1f, timer / duration);
            ping.transform.localScale = initialScale * scaleFactor;
            title.transform.localScale = initialScale * scaleFactor;
            ping.transform.position = Vector3.Lerp(initialPingPosition, targetPingPosition, moveFactor);
            title.transform.position = Vector3.Lerp(initialTitlePosition, targetTitlePosition, moveFactor);

            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ScaleUpCoroutine(idx, mapName));
    }

    private IEnumerator ScaleUpCoroutine(int idx, string mapName)
    {
        float duration = 0.1f;
        float timer = 0f;
        Vector3 initialScale = new Vector3(1.0f, 1.0f, 1.0f);
        GameObject ping = map[idx].transform.GetChild(0).gameObject;
        GameObject title = map[idx].transform.GetChild(1).gameObject;
        Vector3 initialPingPosition = ping.transform.position;
        Vector3 targetPingPosition = initialPingPosition + new Vector3(0, 10, 0);
        Vector3 initialTitlePosition = title.transform.position;
        Vector3 targetTitlePosition = initialTitlePosition + new Vector3(0, -10, 0);

        while (timer < duration)
        {
            float scaleFactor = Mathf.Lerp(0.9f, 1f, timer / duration);
            float moveFactor = Mathf.Lerp(1f, 0f, timer / duration);
            ping.transform.localScale = initialScale * scaleFactor;
            title.transform.localScale = initialScale * scaleFactor;
            ping.transform.position = Vector3.Lerp(targetPingPosition, initialPingPosition, moveFactor);
            title.transform.position = Vector3.Lerp(targetTitlePosition, initialTitlePosition, moveFactor);

            timer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(mapName);
    }
}
