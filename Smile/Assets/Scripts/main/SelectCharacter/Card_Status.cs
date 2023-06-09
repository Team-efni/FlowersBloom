/*
*각 카드의 속성을 부여하는 클래스
*
*구현 목표
*-잠금여부
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using UnityEngine;
using UnityEngine.UI;
public class Card_Status : MonoBehaviour
{
    public Sprite Unlock_image;
    public bool unlocked=false; //false가 잠긴거임

    //잠금 조건 Check
    [Header("아래는 잠금조건입니다.")]
    public bool isEasyClear = false;
    public bool isNormalClear = false;
    public bool isHardClear = false;
}
