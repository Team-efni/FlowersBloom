/*
*모든 게임 내 사운드를 적용하는 스크립트
*
*구현 목표
*-사운드의 값을 전역변수에서 가져옴
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource sound;

    public enum SoundType
    {
        BGM,
        Effect
    }
    public SoundType Sound_type;

    private void Start()
    {
        GetSoundValue();
    }

    private void Update()
    {
        GetSoundValue();
    }

    private void GetSoundValue()
    {
        switch(Sound_type)
        {
            case SoundType.BGM:
                sound.volume = UniteData.BGM;
                break;
            case SoundType.Effect:
                sound.volume = UniteData.Effect;
                break;
            default:
                //Log를 통해 경고한다
                Debug.LogWarning("Sound_Manager class에서 적용하는 SoundType이 설정되지 않았습니다. \n Audio를 관리하는 오브젝트의 Inspector에서 설정을 해주시기 바랍니다.");
                break;
        }
    }
}
