using System.Collections.Generic;
using UnityEngine;

public class UniteData
{
    //게임 관련 데이터
    public static string GameMode = "None"; //현재 게임 모드 ["None", Menu, Play, Pause]
    public static int Difficulty = 5; //게임 난이도 easy = 1 / normal = 2 / hard = 3 / world 2 easy = 4 ...
    public static int notePoint = 2; // 기회 포인트
    public static int lifePoint = 3; // 목숨 포인트

    public static int Node_LifePoint = 2; //노드 목숨
    public static int Node_Click_Counter = 0; //노드 클릭 횟수

    public static bool Move_Progress = true; //Play씬에서 움직임/정지 제어
    public static Vector2 Player_Location_Past = new Vector2(-4955f, 0); //플레이어가 이전에 있던 위치
    public static float Play_Scene_Time = 0f; //플레이 씬에서의 흐른 시간

    public static string Selected_Character = PlayerPrefs.GetString("Selected_Character", "Dandelion"); //유저가 선택한 캐릭터 ["Dandelion", "Tulip"]
    public static string Closed_Monster = "Rose"; //최근접 몬스터 ["Rose", "Cosmos", "MorningGlory", "Poppy"]

    public static bool ReStart = true; // 유저가 재시작 혹은 나가기 버튼을 누른 경우

    public static bool NoteSuccess = false; // 몬스터 상단 노트 성공 유무
    public static bool oneNoteSuccess = false;

    public static List<Dictionary<string, object>> data;
    public static int mon_num = 1; // 현재 나온 마리수

    public static int noteIndex = 0;  // 현재 눌러야할 노트의 자리

    //유저 관련 데이터
    public static int[] GameClear = {
        PlayerPrefs.GetInt("GameClear-Easy", 0), //EASY
        PlayerPrefs.GetInt("GameClear-Normal", 0), //NORMAL
        PlayerPrefs.GetInt("GameClear-Hard", 0), //HARD
        PlayerPrefs.GetInt("GameClear-W2-Easy", 0), //W2E
        PlayerPrefs.GetInt("GameClear-W2-Normal", 0), //W2N
        PlayerPrefs.GetInt("GameClear-W2-Hard", 0) //W2H
}; // 난이도별 클리어 유무 0: false, 1: true

    //설정 관련 데이터
    public static float BGM = PlayerPrefs.GetFloat("BGM", 1f); //배경음악 볼륨
    public static float Effect = PlayerPrefs.GetFloat("Effect", 1f); //효과음 볼륨

    //보안 관련 데이터
    //private static byte[] KEY= System.Text.Encoding.UTF8.GetBytes("68656c6c6f2045666e6920426573746f6d706174657221"); //암호화 키


    //게임 파일 관련 데이터 
    //key-md5 / value-ARIA
    public static void SaveUserData()
    {
        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("Effect", Effect);
        PlayerPrefs.SetInt("GameClear-Easy", GameClear[0]);
        PlayerPrefs.SetInt("GameClear-Normal", GameClear[1]);
        PlayerPrefs.SetInt("GameClear-Hard", GameClear[2]);
        PlayerPrefs.SetInt("GameClear-W2-Easy", GameClear[3]);
        PlayerPrefs.SetInt("GameClear-W2-Normal", GameClear[4]);
        PlayerPrefs.SetInt("GameClear-W2-Hard", GameClear[5]);
        PlayerPrefs.SetString("Selected_Character", Selected_Character);
        PlayerPrefs.Save();
    }
    public static void ResetUserData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("Effect", Effect);

        Selected_Character = "Dandelion";
        GameClear = new int[] { 0, 0, 0, 0, 0, 0 };

        SaveUserData();
        Debug.Log("유저 데이터 초기화 완료");
    }

    //디버깅용 데이터
}
