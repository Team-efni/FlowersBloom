using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackScene : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;
    public void BackSceneBtn()
    {
        //SceneManager.LoadScene("Main Menu");  
        exitPanel.SetActive(false);
    }
}
