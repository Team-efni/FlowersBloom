using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;
    static private GameObject player;

    public GameObject[] canPlay_prefab;
    public GameObject[] backgrounds;

    private float playerStartPositionX;

    private float xScreenSize;
    private float playerMaxMovePosX; // 플레이어 최대 이동 거리

    private bool b_playerMove; // 플레이어 이동 판단 - 게임 재시작시 초기화

    [SerializeField] float moveSpeed; // Floor2의 RepeatBG Speed랑 똑같은 속도로 설정

    public Sprite[] lifepoint_images; // 제공한 목숨 포인트 이미지
    public GameObject[] lifepoints; // 목숨포인트 오브젝트

    private void Awake()
    {
        //캐릭터 지정
        if(UniteData.Selected_Character== "Dandelion")
        {
            //해당 캐릭터를 플레이어로 지정 후 활성화
            canPlay_prefab[0].SetActive(true);
            player = canPlay_prefab[0];
            for(int i = 0; i < lifepoints.Length; i++)
            {
                Image image = lifepoints[i].GetComponent<Image>();
                image.sprite = lifepoint_images[0];
            }
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            //해당 캐릭터를 플레이어로 지정 후 활성화
            canPlay_prefab[1].SetActive(true);
            player = canPlay_prefab[1];
            for (int i = 0; i < lifepoints.Length; i++)
            {
                Image img = lifepoints[i].GetComponent<Image>();
                img.sprite = lifepoint_images[1];
            }
        }
        else
        {
            Debug.LogError("GameClear.cs 파일에서 캐릭터 선택 오류가 발생했습니다 \n 아마도 UniteData.Selected_Character 지정 문제이거나 해당 스크립트의 조건문에서 오류를 수정하세요.");
        }


        // 처음에는 모든 배경 초기화
        for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        // 배경 지정
        switch (UniteData.Difficulty)
        {
            case 1:
                backgrounds[0].SetActive(true);
                break;
            case 2:
                backgrounds[1].SetActive(true);
                break;
            case 3:
                //backgrounds[2].SetActive(true); // 아직 hard 모드가 안나온 상태
                break;
            default:
                backgrounds[0].SetActive(true);
                break;
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
