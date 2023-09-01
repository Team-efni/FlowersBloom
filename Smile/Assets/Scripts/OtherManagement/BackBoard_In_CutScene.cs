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

    private enum Banner
    {
        Dandelion,
        Tulip,
        Rose,
        Cosmos,
        MorningGlory,
        Poppy,

        ForgetMeNot

    }

    private void Start()
    {
        Set_Image();
    }

    private void Update()
    {
        Set_Image();
    }

    private int Find_Index(string name)
    {
        Banner banner;
        if (!System.Enum.TryParse(name, out banner))
        {
            Debug.LogWarning("해당 이름을 가진 이미지 파일이 적용되지 않았습니다.\nBackBoard 오브젝트에서 확인하세요!");
            return -1;  // 인자로 전달된 문자열이 enum에 정의되어 있지 않은 경우 -1 반환
        }

        switch (banner)
        {
            case Banner.Dandelion:
                return 0;
            case Banner.Tulip:
                return 1;
            case Banner.Rose:
                return 2;
            case Banner.Cosmos:
                return 3;
            case Banner.MorningGlory:
                return 4;
            case Banner.Poppy:
                return 5;
            case Banner.ForgetMeNot:
                return 6;

            default:
                Debug.LogWarning("해당 이름을 가진 이미지 파일이 적용되지 않았습니다.\nBackBoard 오브젝트에서 확인하세요!");
                return -1;
        }
    }

    private void Set_Image()
    {
        BackBoardRenderer_Top.sprite = BackBoard[Find_Index(UniteData.Closed_Monster)];
        BackBoardRenderer_Bottom.sprite = BackBoard[Find_Index(UniteData.Selected_Character)];
    }
}
