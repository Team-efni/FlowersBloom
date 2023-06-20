using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "GSBtn":
                SceneManager.LoadScene("Map Menu");
                break;
            case "CharBtn":
                SceneManager.LoadScene("Character Menu");
                break;
            case "Map1Btn":
                SceneManager.LoadScene("Map1 Menu");
                break;
            case "Map2Btn":
                SceneManager.LoadScene("Map2 Menu");
                break;
        }
    }  
}
