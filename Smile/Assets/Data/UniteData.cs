using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteData
{
    //게임 관련 데이터
    public static string GameMode = "None"; //현재 게임 모드 ["None", Menu, Play, CutScene, setting]
    public static int Difficulty = 1; //게임 난이도
    public static int notePoint = 2; // 기회 포인트
    public static int lifePoint = 3; // 목숨 포인트

    public static int Node_LifePoint = 2; //노드 목숨
    public static int Node_Click_Counter = 0; //노드 클릭 횟수

    public static bool Move_Progress = true; //Play씬에서 움직임/정지 제어
    public static Vector2 Player_Location_Past= new Vector2 (-4955f, 0); //플레이어가 이전에 있던 위치
    public static float Play_Scene_Time = 0f; //플레이 씬에서의 흐른 시건

    public static string Selected_Character = "Dandelion"; //유저가 선택한 캐릭터 ["Dandelion", "Tulip"]
    public static string Closed_Monster = "Rose"; //최근접 몬스터 ["Rose", "Kosmos", "MorningGlory"]

    public static bool ReStart = false; // 유저가 재시작 버튼을 누른 경우

    //유저 관련 데이터
    public static bool[] GameClear = { false, false, false}; // 난이도별 클리어 유무

    //설정 관련 데이터
    public static float BGM = 1f; //배경음악 볼륨
    public static float Effect = 1f; //효과음 볼륨

    //게임 파일 관련 데이터

}
