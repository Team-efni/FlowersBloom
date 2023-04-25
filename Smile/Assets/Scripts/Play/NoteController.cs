using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public Sprite[] noteSprite;
    public GameObject[] note;
    public int[] noteNums;
    public bool meetMonster = false;
    private int noteIndex = 0;  // 현재 눌러야할 노트의 자리

    public GameObject MonsterParent;

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
    {// 노트 반투명으로 만들기
        //note[noteIndex].SetActive(false);
        Image image = note[noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
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
            MonsterParent.SetActive(false);
            DoBgShow(false); // 상단 노트 UI 비활성화
        }
    }

    private void DoBgShow(bool check)
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(check); // Note_Bg
    }
}
