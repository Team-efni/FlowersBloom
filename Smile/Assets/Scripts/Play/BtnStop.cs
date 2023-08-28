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
        Audio_Manager.play_Audio = false;
        ShowPanel();
    }

    public void ResumePlay()
    {
        stopPanel.SetActive(false);
        Debug.Log(UniteData.GameMode);
        if(UniteData.GameMode!= "Scripting")
            Time.timeScale = 1.0f;
        UniteData.Move_Progress = true; // 움직임 재개
        Audio_Manager.play_Audio = true;
    }

    public void GoMap()
    {
        SceneManager.LoadScene("Map1 Menu");
        Time.timeScale = 1.0f;
        UniteData.ReStart = true;
        UniteData.Move_Progress = true;
        Audio_Manager.play_Audio = true;
    }
}
