/*
*스크롤 값을 통합적으로 제어하는 스크립트
*
*구현 목표
*-특정 게이지에 접근 가능하도록 설정
*-초기 접속 시 게이지 값 반영
*-게이지 값 변경 시 전역변수에 값을 입력
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGaugeValue : MonoBehaviour
{
    public Slider slider;
    public enum GaugeType
    {
        BGM,
        Effect
    }

    public GaugeType type;

    private void Start()
    {
        approachStaticValue();
        slider.value = approachStaticValue();
    }

    private void Update()
    {
        gaugeChangeValue(slider.value);
    }

    private void gaugeChangeValue(float gval)
    {
        switch (type)
        {
            case GaugeType.BGM:
                UniteData.BGM = gval;
                break;
            case GaugeType.Effect:
                UniteData.Effect = gval;
                break;
            default:
                break;
        }
    }

    private float approachStaticValue()
    {
        switch(type)
        {
            case GaugeType.BGM:
                return UniteData.BGM;
            case GaugeType.Effect:
                return UniteData.Effect;
            default:
                return 1f;
        }
    }
}
