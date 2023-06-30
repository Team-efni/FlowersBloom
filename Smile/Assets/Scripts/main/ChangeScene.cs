using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator animator;

    public void initAnim()
    {
        animator.GetInteger("LocalStage");
        animator.SetInteger("LocalStage", 0);
    }

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "GSBtn":
                SceneManager.LoadScene("Map1 Menu");
                break;
            case "CharBtn":
                SceneManager.LoadScene("Character Menu");
                break;
            case "Map1Btn":
                Debug.Log("Map1Btn");
                animator.SetInteger("LocalStage", 2);
                //SceneManager.LoadScene("Map1 Menu");
                break;
            case "Map2Btn":
                animator.SetInteger("LocalStage", 1);
                //SceneManager.LoadScene("Map2 Menu");
                break;
        }
    }  
}
