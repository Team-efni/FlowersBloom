using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            case BTNType.Exit:
                Debug.Log("종료");
                break;
            case BTNType.Sound:
                Debug.Log("사운드");
                break;
            case BTNType.Teammate:
                Debug.Log("팀원");
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
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

 
}
