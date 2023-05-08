using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChange : MonoBehaviour
{
    public SoundManager soundManager;

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
        SceneManager.LoadScene("Easy Map");
    }

    public void NormalBtn()
    {
        UniteData.Difficulty = 2;
        SceneManager.LoadScene("Nomal Map");
    }

    public void HardBtn()
    {
        UniteData.Difficulty = 3;
    }
}
