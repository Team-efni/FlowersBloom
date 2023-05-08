using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidExit : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;
    //[SerializeField] private string destination;
    private void Start()
    {
        exitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
            //SceneManager.LoadScene(destination);
        }
          
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {

            }
        }
    }

    public void noBtn()
    {
        exitPanel.SetActive(false);
    }
}
