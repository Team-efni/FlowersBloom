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
}
