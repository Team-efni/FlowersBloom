using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnStop : MonoBehaviour
{
    public GameObject stopPanel;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        stopPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        Debug.Log("ShowPanel");
        stopPanel.SetActive(true);
    }

    public void StopPlay()
    {
        Time.timeScale = 0f;
        UniteData.Move_Progress = false; // 움직임 정지
        ShowPanel();
    }

    public void ResumePlay()
    {
        stopPanel.SetActive(false);
        Time.timeScale = 1.0f;
        UniteData.Move_Progress = true; // 움직임 재개
    }

    public void GoMap()
    {
        SceneManager.LoadScene("Map Menu");
        UniteData.ReStart = true;
        UniteData.Move_Progress = true;
    }
}
