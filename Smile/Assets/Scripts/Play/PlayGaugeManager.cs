/*
*play 씬에서 게임의 진척도를 표기하는 스크립트
*
*구현 목표
*현 플레이어의 진행 수준을 시현
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
//디버깅용 데이터
#define RELEASE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGaugeManager : MonoBehaviour
{
    public Slider slider;

    private float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        //access deny about slider controll by player
        slider.interactable = false;

        //난이도에 따라 최대 시간 변경
        switch (UniteData.Difficulty)
        {
            case 1:
                maxTime = 52f;
                break;
            case 2:
                maxTime = 52f;
                break;
            case 3:
                maxTime = 52f;
                break;
            case 4:
                maxTime = 52f;
                break;
            case 5:
                maxTime = 64f;
                break;
            case 6:
                maxTime = 52f;
                break;
            default:
                maxTime = 52f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UniteData.Play_Scene_Time);
        slider.value = UniteData.Play_Scene_Time / maxTime;
    }
}
