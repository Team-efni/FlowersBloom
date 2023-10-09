using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_GuideManager : MonoBehaviour
{
    [Header("가이드 텍스트 박스 UI")] public GameObject guideTextBox;
    [Header("가이드 텍스트 UI")] public Text guideText;
    // Start is called before the first frame update
    void Start()
    {
        guideTextBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 몬스터 조우 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !UniteData.ReStart)
        {
            // 첫번째 몬스터 조우 시
            if (UniteData.mon_num == 1)
            {
                // 텍스트 박스 표시
                guideTextBox.SetActive(true);
                guideText.text = "노트박스에 표시되는 색상 순서대로\r\n좌, 우의 노트를 터치하세요.";
                
                // 이동 멈추기
                UniteData.Move_Progress = false;
            }
        }
    }
}
