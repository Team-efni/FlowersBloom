using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject[] map = new GameObject[6];
    private Vector3[] pos = new Vector3[6];

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

    private void Start()
    {
        pos[0] = new Vector3(-50f, 20f, 0f);
        pos[1] = new Vector3(-10f, 0f, 0f);
        pos[2] = new Vector3(50f, 10f, 0f);
        pos[3] = new Vector3(-50f, 5f, 0f);
        pos[4] = new Vector3(-10f, 5f, 0f);
        pos[5] = new Vector3(50f, 10f, 0f);
    }

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
        Vector3 initialPosition = map[idx].transform.position;
        Vector3 targetPosition = initialPosition + pos[idx];

        while (timer < duration)
        {
            float scaleFactor = Mathf.Lerp(1f, 0.9f, timer / duration);
            float moveFactor = Mathf.Lerp(0f, 1f, timer / duration);
            map[idx].transform.localScale = initialScale * scaleFactor;
            map[idx].transform.position = Vector3.Lerp(initialPosition, targetPosition, moveFactor);

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
        Vector3 initialPosition = map[idx].transform.position;
        Vector3 targetPosition = initialPosition - pos[idx];

        while (timer < duration)
        {
            float scaleFactor = Mathf.Lerp(0.9f, 1f, timer / duration);
            float moveFactor = Mathf.Lerp(1f, 0f, timer / duration);
            map[idx].transform.localScale = initialScale * scaleFactor;
            map[idx].transform.position = Vector3.Lerp(targetPosition, initialPosition, moveFactor);

            timer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(mapName);
    }
}
