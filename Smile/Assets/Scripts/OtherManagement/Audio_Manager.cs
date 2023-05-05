/*
*오디오를 통합적으로 제어하는 스크립트
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

    public static Audio_Manager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
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
        if (SceneManager.GetActiveScene().name != "Play" && SceneManager.GetActiveScene().name != "InGame-RN")
        {
            Destroy(gameObject);
        }

        AudioSource ass = gameObject.GetComponent<AudioSource>();
        ass.volume = UniteData.BGM;
    }
}
