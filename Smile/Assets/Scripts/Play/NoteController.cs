using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public Sprite[] noteSprite;
    public GameObject[] note;
    public int[] noteNums;
    public bool meetMonster = false;
    private int noteIndex = 0;  // 현재 눌러야할 노트의 자리

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        noteIndex = 0;
        meetMonster = false;
        DoBgShow(false); // 시작할 때는 상단 노트 UI 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Meet");
            NoteSetting();
            DoBgShow(true); // 상단 노트 UI 활성화
            meetMonster = true;
        }
    }

    private void NoteSetting()
    {
        noteNums = new int[note.Length];

        // 랜덤으로 노트 생성
        for (int i = 0; i < note.Length; i++)
        {
            noteNums[i] = Random.Range(0, 4);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
        }
    }

    public void NoteDisabled()
    {
        // 노트 회색으로 만들기
        Image image = note[noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
    }

    public void NoteAbled()
    {
        // 노트 원래색으로 만들기
        for (int i = 0; i < note.Length; i++)
        {
            Image image = note[i].GetComponent<Image>();
            image.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void touchClickLeftUp()
    {
        Debug.Log("touchClickLeftUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 0)
                NoteSuccess();
        }
            
    }

    public void touchClickLeftDown()
    {
        Debug.Log("touchClickLeftDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 1)
                NoteSuccess();
        }
            
    }

    public void touchClickRightUp()
    {
        Debug.Log("touchClickRightUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 2)
                NoteSuccess();
        }
            
    }

    public void touchClickRightDown()
    {
        Debug.Log("touchClickRightDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 3)
                NoteSuccess();
        }
    }

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        noteIndex++;

        if (noteIndex == note.Length)
        {
            // 모두 성공한 경우
            Debug.Log("All Success");
            meetMonster = false;
            MonsterDie();
            DoBgShow(false); // 상단 노트 UI 비활성화
            returnNote();
        }
    }

    private void DoBgShow(bool check)
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(check); // Note_Bg
    }

    // 몬스터 죽기
    private void MonsterDie()
    {
        StartCoroutine("MonsterFadeOut");
    }

    // 몬스터 페이드 아웃 처리
    IEnumerator MonsterFadeOut()
    {
        int i = 10;
        while(i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = target.GetComponent<SpriteRenderer>().color;
            c.a = f;
            target.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.02f);
        }

        target.gameObject.SetActive(false);
    }

    // 노트들 처음 상태로 되돌리기
    void returnNote()
    {
        noteIndex = 0;
        NoteAbled();
    }
}
