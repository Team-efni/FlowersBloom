using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;
    public GameObject player;

    public GameObject[] canPlay_prefab;

    private float playerStartPositionX;

    private float xScreenSize;
    private float playerMaxMovePosX; // 플레이어 최대 이동 거리

    private bool b_playerMove; // 플레이어 이동 판단 - 게임 재시작시 초기화

    [SerializeField] float moveSpeed; // Floor2의 RepeatBG Speed랑 똑같은 속도로 설정


    private void Awake()
    {
        //캐릭터 지정
        if(UniteData.Selected_Character== "Dandelion")
        {
            //해당 캐릭터를 플레이어로 지정 후 활성화
            canPlay_prefab[0].SetActive(true);
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            //해당 캐릭터를 플레이어로 지정 후 활성화
            canPlay_prefab[1].SetActive(true);
        }
        else
        {
            Debug.LogError("GameClear.cs 파일에서 캐릭터 선택 오류가 발생했습니다 \n 아마도 UniteData.Selected_Character 지정 문제이거나 해당 스크립트의 조건문에서 오류를 수정하세요.");
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
        if(b_playerMove)
        {
            if(player.transform.position.x < playerMaxMovePosX)
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
