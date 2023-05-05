/*
*CutScene 돌입 시 백보드(백그라운드)의 이미지를 지정하는 스크립트
*
*구현 목표
*-씬 돌입 시 상황에 따라 이미지 변경을 가변적으로 시행
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoard_In_CutScene : MonoBehaviour
{
    public Sprite[] BackBoard;
    public SpriteRenderer BackBoardRenderer_Top;
    public SpriteRenderer BackBoardRenderer_Bottom;

    public void Start()
    {
        BackBoardRenderer_Top.sprite = BackBoard[Find_Index(UniteData.Closed_Monster)];
        BackBoardRenderer_Bottom.sprite = BackBoard[Find_Index(UniteData.Selected_Character)];
    }

    private int Find_Index(string name)// 너무 느낌 없이 작성했다... [HACK]
    {
        if(name== "Dandelion")
        {
            return 0;
        }
        else if(name== "Tulip")
        {
            return 1;
        }

        else if(name=="Rose")
        {
            return 2;
        }

        else if(name=="Cosmos")
        {
            return 3;
        }

        return 0;
    }
}
