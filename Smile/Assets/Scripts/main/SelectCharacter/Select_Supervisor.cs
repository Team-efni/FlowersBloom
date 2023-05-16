/*
*캐릭터 선택창을 감독하는 스크립트
*
*구현 목표
*-캐릭터 잠금여부 부여
*-카드 클릭 시 변경
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Supervisor : MonoBehaviour
{
    public GameObject Select_Icon;
    public GameObject[] Card;

    // Start is called before the first frame update
    void Start()
    {
        //선택 가능 캐릭터 활성화
        foreach(GameObject gm in Card)
        {
            CheckList(gm);
        }
    }

    private void Update()
    {
        //선택된 캐릭터 정보 체크 후 강조
        foreach (GameObject Cname in Card)
        {
            if (Cname.name == UniteData.Selected_Character)
            {
                Select_Icon.transform.position = Cname.transform.position;
            }
        }
    }

    private void CheckList(GameObject character_card)
    {
        Card_Status CS = character_card.GetComponent<Card_Status>();

        //잠금 여부 결정
        unlock_Tulip(CS, character_card);

        //잠금이 풀린 상황이면
        if (CS.unlocked)
        {
            Debug.Log(character_card.name + " 활성화");
        }
    }

    //카드를 클릭했을 때 처리 
    public void pressCharacterCard(GameObject character_card)
    {
        Card_Status CS=character_card.GetComponent<Card_Status>();

        //잠금이 풀린 상황이면
        if(CS.unlocked)
        {
            //선택가능하게 한다
            UniteData.Selected_Character = character_card.name;
            Debug.Log(UniteData.Selected_Character+"으로 선택했습니다.");
        }
        else
        {
            Debug.Log("응~ 아직 안돼~");
        }

        UniteData.SaveUserData();
    }

    private void unlock_Tulip(Card_Status CS, GameObject Card)
    {
        //Easy 모드를 클리어 했을 때
        if (CS.isEasyClear == (UniteData.GameClear[0] == 1) && CS.isEasyClear) 
        {
            //잠금 해제
            CS.unlocked = true;

            Image image = Card.GetComponent<Image>();
            image.sprite = CS.Unlock_image;
        }
    }

}
