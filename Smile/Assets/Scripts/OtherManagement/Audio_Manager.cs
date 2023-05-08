/*
*BGM 관련 오디오를 통합적으로 제어하는 스크립트
*
*구현 목표
*-싱글톤으로 구현하여 어디서든 접근 가능하게 만들기
*-BGM의 경우 씬이 바뀌어도 멈추지 않고 계속 재생되게 만들기
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance = null;

    public AudioClip[] audioList;
    private AudioSource ass;
    public static bool play_Audio = true;

    private int sceneGroup = -1;


    public static Audio_Manager Instance
    {
        get { return instance; }
    }

    void 씬바뀌면딱한번만실행()
    {
        if(sceneGroup != getSceneGroup(SceneManager.GetActiveScene().name))
        {
            sceneGroup = getSceneGroup(SceneManager.GetActiveScene().name);
            //Debug.Log("NAME: " + SceneManager.GetActiveScene().name+" GR: "+ getSceneGroup(SceneManager.GetActiveScene().name));
            BGM_Change(sceneGroup);
        }
        return;
    }

    void Awake()
    {
        ass = gameObject.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        씬바뀌면딱한번만실행();

        ass.volume = UniteData.BGM;

        if(play_Audio)
        {
            ass.pitch = 1f;
        }
        else
        {
            ass.pitch = 0f;
        }
    }

    private void BGM_Change(int index)
    {
        ass.Stop();
        ass.clip = audioList[index];
        ass.Play();
    }

    private int getSceneGroup(string scenename)
    {
        //play / cut 씬에서의 BGM을 설정
        if (scenename == "Play" || scenename == "InGame-RN")
        {
            return 1;
        }
        //메인화면 / 맵 선택 / 스테이지 돌입 전 BGM을 설정
        else if (scenename == "Main Manu" ||
                scenename == "Character Menu" ||
                scenename == "MapManu/Map Menu" ||
                scenename == "MapManu/Easy Map" ||
                scenename == "MapManu/Nomal Map")
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
}
