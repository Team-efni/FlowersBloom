using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;
    public GameObject player;

    private float playerStartPositionX;

    private float xScreenSize;
    private float playerMaxMovePosX; // 플레이어 최대 이동 거리

    private bool b_playerMove; // 플레이어 이동 판단 - 게임 재시작시 초기화

    [SerializeField] float moveSpeed; // Floor2의 RepeatBG Speed랑 똑같은 속도로 설정

    // Start is called before the first frame update
    void Start()
    {
        Initialized();

        xScreenSize = Camera.main.orthographicSize * Camera.main.aspect * 2;
        playerStartPositionX = player.transform.position.x;
        playerMaxMovePosX = playerStartPositionX + xScreenSize;
    }

    public void Initialized()
    {
        b_playerMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_playerMove)
        {
            if(player.transform.position.x < playerMaxMovePosX)
            {
                PlayerMove();
            }

            // 화면 밖에 도착하면 페이드 아웃 애니메이션 후 씬 전환
            else
            {
                Animator fadeAnimator = GameObject.Find("FadeOut_Clear").GetComponent<Animator>();
                
                fadeAnimator.SetBool("IsStartFade", true);
            }
            
        }
    }

    public void ClearGame()
    {
        bgStop();
        UniteData.Move_Progress = false;
        UniteData.GameClear[UniteData.Difficulty] = true;
        b_playerMove = true;
    }

    private void bgStop()
    {
        GameObject[] Background = GameObject.FindGameObjectsWithTag("Background");

        for (int i = 0; i < Background.Length; i++)
        {
            Background[i].GetComponent<RepeatBG>().SetGameClearTrue();
        }
    }

    private void PlayerMove()
    {
        player.transform.position = player.transform.position + (player.transform.right * moveSpeed * Time.deltaTime);
    }
}
