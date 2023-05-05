using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "EasyBtn":
                SceneManager.LoadScene("Easy Map");
                break;
            case "NomalBtn":
                SceneManager.LoadScene("Nomal Map");
                break;
        }
    }
}
