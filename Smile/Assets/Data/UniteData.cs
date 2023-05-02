using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteData
{
    //게임 관련 데이터
    public static int Difficulty = 2; //게임 난이도
    public static int notePoint = 2; // 기회 포인트
    public static int lifePoint = 3; // 목숨 포인트

    public static int Node_LifePoint = 2; //노드 목숨
    public static int Node_Click_Counter = 0; //노드 클릭 횟수

    public static bool Move_Progress = true; //Play씬에서 움직임/정지 제어

    //유저 관련 데이터
    public static bool[] GameClear = { false, false, false}; // 난이도별 클리어 유무

    //설정 관련 데이터
    public static float BGM = 1f; //배경음악 볼륨
    public static float Effect = 1f; //효과음 볼륨

    //게임 파일 관련 데이터

}
