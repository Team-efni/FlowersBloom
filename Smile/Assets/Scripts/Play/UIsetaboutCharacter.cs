/*
*play 진입 시 캐릭터에 따른 UI 정보를 변경하는 스크립트
*
*구현 목표
*-캐릭터에 따라 UI를 변경한다
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIsetaboutCharacter : MonoBehaviour
{
    public GameObject[] Life_UI_Object;

    public Sprite 민들레_이미지;
    public Sprite 튤립_이미지;

    // Start is called before the first frame update
    void Awake()
    {
        if(UniteData.Selected_Character=="Dandelion")
        {
            foreach(GameObject ui in Life_UI_Object)
            {
                ui.GetComponent<Image>().sprite = 민들레_이미지;
            }
        }
        else if(UniteData.Selected_Character=="Tulip")
        {
            foreach (GameObject ui in Life_UI_Object)
            {
                ui.GetComponent<Image>().sprite = 튤립_이미지;
            }
        }
    }
}
