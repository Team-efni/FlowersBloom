using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapBack : MonoBehaviour
{
    public void BackSceneBtn()
    {
        SceneManager.LoadScene("Map1 Menu");  
    }
}
