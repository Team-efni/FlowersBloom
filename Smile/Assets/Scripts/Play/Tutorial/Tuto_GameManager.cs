using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_GameManager : MonoBehaviour
{
    static public GameObject player;
    private bool b_playerMove; // 플레이어 이동 판단 - 게임 재시작시 초기화

    public GameObject Player_prefab;

    private float playerStartPositionX;
    private float xScreenSize;
    private float playerMaxMovePosX; // 플레이어 최대 이동 거리

    public Sprite lifepoint_images; // 제공한 목숨 포인트 이미지
    public GameObject[] lifepoints; // 목숨포인트 오브젝트

    [SerializeField] float moveSpeed; // Floor2의 RepeatBG Speed랑 똑같은 속도로 설정

    private void Awake()
    {
        UniteData.finishGame = false;

        Player_prefab.SetActive(true);
        player = Player_prefab;

        for (int i = 0; i < lifepoints.Length; i++)
        {
            Image img = lifepoints[i].GetComponent<Image>();
            img.sprite = lifepoint_images;
        }
    }

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
        if (b_playerMove)
        {
            if (player.transform.position.x < playerMaxMovePosX)
            {
                PlayerMove();
            }

            if (player.transform.position.x > playerMaxMovePosX - 1500)
            {
                Animator fadeAnimator = GameObject.Find("FadeOut_Clear").GetComponent<Animator>();

                fadeAnimator.SetBool("IsStartFade", true);
            }
        }
    }

    public void ClearGame()
    {
        UniteData.finishGame = true;
        bgStop();

        //스토리 스크립트 종료
        //storyScriptGroup.SetActive(true);

        UniteData.ReStart = true;
        UniteData.Move_Progress = false;
        //UniteData.GameClear[UniteData.Difficulty - 1] = 1; // 튜토리얼도 게임 클리어 판정해야하나??
        //UniteData.SaveUserData();
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
