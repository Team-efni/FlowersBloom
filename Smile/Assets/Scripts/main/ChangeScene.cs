using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator animator;
    public GameObject targetObject;

    private static int localStageCount;
    private string targetSceneName;

    private void Start()
    {
        try
        {
            RectTransform _transform;
            _transform = targetObject.GetComponent<RectTransform>();
            if (localStageCount == 1)
            {
                _transform.anchoredPosition = new Vector2(-3200, 0);
                Debug.Log("localStageCount 1");
            }
            else if (localStageCount == 2)
            {
                _transform.anchoredPosition = new Vector2(0, 0);
            }
        }
        catch
        {

        }
    }

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "GSBtn":
                targetSceneName = "Map1 Menu";
                Invoke("LoadSceneDelayed", 0.15f);
                break;
            case "CharBtn":
                targetSceneName = "Character Menu";
                Invoke("LoadSceneDelayed", 0.15f);
                break;
            case "Map1Btn":
                Debug.Log("Map1Btn");
                localStageCount = 2;
                animator.SetInteger("LocalStage", localStageCount);
                //SceneManager.LoadScene("Map1 Menu");
                break;
            case "Map2Btn":
                localStageCount = 1;
                animator.SetInteger("LocalStage", localStageCount);
                //SceneManager.LoadScene("Map2 Menu");
                break;
        }
    }

    public void LoadSceneDelayed()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
