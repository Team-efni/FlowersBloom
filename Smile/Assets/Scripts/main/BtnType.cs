using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defualtScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

   
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.GS:
                Debug.Log("게임시작");
                break;
            case BTNType.Char:
                Debug.Log("캐릭터");
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Sound:
                Debug.Log("사운드");
                break;
            case BTNType.Teammate:
                Debug.Log("팀원");
                break;
            case BTNType.Back:
                SaveSettingData();
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Reset:
                UniteData.ResetUserData();
                break;
        }
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    //설정에서 나갈 시 모든 데이터를 저장합니다
    private void SaveSettingData()
    {
        Slider BGM=GameObject.Find("MusicSlider").GetComponent<Slider>();
        UniteData.BGM = BGM.value;

        Slider Effect=GameObject.Find("SoundEffectSlider").GetComponent<Slider>();
        UniteData.Effect = Effect.value;

        UnityEngine.Debug.Log(UniteData.BGM + " / " + UniteData.Effect);
        UniteData.SaveUserData();
    }    
}
