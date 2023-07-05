using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameclearManager : MonoBehaviour
{
    private ReStartGame s_rsg;
    // Start is called before the first frame update
    void Start()
    {
        //전역변수 초기화
        UniteData.notePoint = 2; // 기회 포인트
        UniteData.lifePoint = 3; // 목숨 포인트    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RetryGame()
    {
        UniteData.ReStart = true;
        SceneManager.LoadScene("Play");
    }

    public void NextStage()
    {
        UniteData.Difficulty++;

        UniteData.Move_Progress = true;
        UniteData.lifePoint = 3;
        UniteData.notePoint = 2;
        UniteData.Play_Scene_Time = 0f;
        UniteData.Player_Location_Past = Vector2.zero;
        UniteData.ReStart = true;

        if(UniteData.Difficulty < 5) // world 2 easy 이후로는 없어서
            SceneManager.LoadScene("Play");

    }
}
