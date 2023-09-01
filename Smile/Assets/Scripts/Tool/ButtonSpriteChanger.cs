using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    public Sprite normalSprite;
    public Sprite pressedSprite;

    void Start()
    {
        // 버튼의 일반 상태 스프라이트 설정
        button.GetComponent<Image>().sprite = normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //애니메이션으로 이미지 체인지 목표[FIXME]
        // 버튼을 누르고 홀드할 때 스프라이트 변경
        button.GetComponent<Image>().sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 버튼에서 손을 떼면 스프라이트를 일반 상태로 변경
        button.GetComponent<Image>().sprite = normalSprite;
    }
}
